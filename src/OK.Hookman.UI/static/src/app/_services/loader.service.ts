import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class LoaderService {

    public onToggle: Subject<boolean> = new Subject<boolean>();
    private count = 0;
    private minimumDuration = 1000;
    private showDate: Date;

    public show(): void {
        if (this.count <= 0) {
            this.onToggle.next(true);
            this.showDate = new Date();

            this.count = 1;
        } else {
            this.count++;
        }
    }

    public hide(): void {
        this.count--;

        if (this.count <= 0) {
            const diff = new Date().getTime() - this.showDate.getTime();
            if (diff >= this.minimumDuration) {
                this.onToggle.next(false);
            } else {
                setTimeout(() => {
                    this.onToggle.next(false);
                }, this.minimumDuration - diff);
            }

            this.count = 0;
        }
    }

}
