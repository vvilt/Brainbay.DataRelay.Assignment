using Brainbay.DataRelay.Domain;
using Brainbay.DataRelay.Domain.Models;
using Brainbay.DataRelay.Domain.Repositories;
using Brainbay.DataRelay.Sync.Mapping;
using Brainbay.DataRelay.Sync.ServiceClients;
using Microsoft.Extensions.Options;
namespace Brainbay.DataRelay.Sync;

public class Synchronizer<TDomain, TApi> : ISynchronizer<TDomain, TApi> where TDomain : class, IIdentifiable
{
    private readonly IAPIClient _apiClient;
    private readonly IRepository<TDomain> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper<TDomain, TApi> _mapper;
    private readonly ApiClientConfiguration _configuration;

    public Synchronizer(IAPIClient apiClient
        , IRepository<TDomain> repository
        , IUnitOfWork unitOfWork
        , IOptions<ApiClientConfiguration> options
        , IMapper<TDomain, TApi> mapper)
    {
        _apiClient = apiClient;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = options.Value;
    }
    public async Task SyncAsync(Action<TDomain, TApi> beforeSave = null
        , Action<TDomain, TApi> afterSave = null
        , Func<TApi, bool> filterApiResourcePredicate = null
        , CancellationToken cancellationToken = default)
    {
        var apiTypeName = typeof(TApi).Name;
        _configuration.Resources.TryGetValue(apiTypeName, out var resourceSource);

        if (resourceSource == null)
        {
            throw new ArgumentOutOfRangeException(@$"Configuration for api type '{apiTypeName}' 
                                                                is missed in configuration.");
        }

        var pageableResource = await SyncBatch(resourceSource.InitialUri,
            beforeSave,
            afterSave,
            filterApiResourcePredicate,
            cancellationToken);

        while (!string.IsNullOrWhiteSpace(pageableResource.Info.Next))
        {
            pageableResource = await SyncBatch(pageableResource.Info.Next,
                beforeSave,
                afterSave,
                filterApiResourcePredicate,
                cancellationToken);
        }

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    private async Task<ApiResponse<TApi>> SyncBatch(string resourceSource,
        Action<TDomain, TApi> beforeSave,
        Action<TDomain, TApi> afterSave,
        Func<TApi, bool> filterApiResourcePredicate,
        CancellationToken cancellationToken)
    {
        var pageableResource = await _apiClient
            .GetPageableAsync<TApi>(resourceSource, cancellationToken);

        foreach (var apiResource in pageableResource.Results)
        {
            if (filterApiResourcePredicate != null && !filterApiResourcePredicate(apiResource))
            {
                continue;
            }

            var domain = _mapper.Map(apiResource);

            if (beforeSave != null)
            {
                beforeSave(domain, apiResource);
            }

            domain.Id = await _repository.InsertAsync(domain, cancellationToken);

            if (afterSave != null)
            {
                afterSave(domain, apiResource);
            }
        }

        return pageableResource;
    }
}