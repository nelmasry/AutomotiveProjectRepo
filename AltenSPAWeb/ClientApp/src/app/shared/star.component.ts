import { Component, OnChanges, Input, EventEmitter, Output } from '@angular/core';

@Component({
    selector: 'pm-star',
    templateUrl: './star.component.html',
    styleUrls: ['./star.component.css']
})

export class StarComponent implements OnChanges{
    @Input() rating: number;
    starWidth: number;
    @Output() ratingClicked: EventEmitter<string> =
        new EventEmitter<string>();
    ngOnChanges(): void {
        this.starWidth = this.rating * 75 / 5;
    }
    onClick(): void{
        var message:string = `The rating ${this.rating} is clicked!`;
        this.ratingClicked.emit(message);
        console.log(message);
    }
}