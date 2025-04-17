import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { CharacterService } from '../services/character.service';
import { LocationService } from '../services/location.service'
import { LocationIdName } from '../models/location-id-name.model';

@Component({
    selector: 'app-character-add',
    templateUrl: './character-add.component.html',
    standalone: false,
    styleUrls: ['./character-add.component.scss']
})
export class CharacterAddComponent implements OnInit {
    character: any = {
        name: '',
        locationId: null,
        originId: null,
    };

    locationControl: FormControl<string | LocationIdName> = new FormControl('');
    originControl: FormControl<string | LocationIdName> = new FormControl('');
    availableLocations: LocationIdName[] = [];
    filteredLocations!: Observable<LocationIdName[]>;
    filteredOrigins!: Observable<LocationIdName[]>;

    successMessage: string = '';
    errorMessage: string = '';

    constructor(private characterService: CharacterService, private locationService: LocationService) { }

    ngOnInit(): void {
        this.locationService.getLocationIdName().subscribe({
            next: (locations: LocationIdName[]) => {
                this.availableLocations = locations;
                this.filteredLocations = this.locationControl.valueChanges.pipe(
                    startWith(''),
                    map(value => (typeof value === 'string' ? value : value?.name)),
                    map(name => name ? this._filterLocations(name) : this.availableLocations.slice())
                );
                this.filteredOrigins = this.originControl.valueChanges.pipe(
                    startWith(''),
                    map(value => typeof value === 'string' ? value : value?.name),
                    map(name => name ? this._filterLocations(name) : locations.slice())
                );
            },
            error: err => {
                this.errorMessage = 'Error loading locations';
            }
        });
    }

    private _filterLocations(name: string): LocationIdName[] {
        const filterValue = name.toLowerCase();
        return this.availableLocations.filter(loc => loc.name.toLowerCase().includes(filterValue));
    }

    displayLocation(location: LocationIdName): string {
        return location && location.name ? location.name : '';
    }

    onLocationSelected(location: LocationIdName) {
        this.character.locationId = location.id;
    }

    onOriginSelected(location: LocationIdName) {
        this.character.originId = location.id;
    }

    onAutocompleteClosed(type: 'location' | 'origin'): void {
        if (type === 'location') {
            if (typeof this.locationControl.value === 'string') {
                this.locationControl.setValue('');
                this.character.locationId = null;
            }
        } else if (type === 'origin') {
            if (typeof this.originControl.value === 'string') {
                this.originControl.setValue('');
                this.character.originId = null;
            }
        }
    }

    onSubmit(form: NgForm): void {
        if (form.valid) {
            this.characterService.addCharacter(this.character)
                .subscribe({
                    next: response => {
                        this.successMessage = 'Character added successfully!';
                        this.errorMessage = '';
                        form.resetForm();
                        this.locationControl.reset('');
                        this.originControl.reset('');
                        this.character = {
                            name: '',
                            locationId: null,
                            originId: null
                        }
                    },
                    error: err => {
                        this.errorMessage = 'Error adding character';
                        this.successMessage = '';
                    }
                });
        }
    }
}
