using System.ComponentModel.DataAnnotations;

namespace Brainbay.DataRelay.Sync;

public class ApiClientConfiguration
{
    public Dictionary<string, ResourceSource> Resources { get; set; }

    public class ResourceSource
    {
        [Required(ErrorMessage = "OptionA is required.")]
        public string InitialUri { get; set; }
    }
}