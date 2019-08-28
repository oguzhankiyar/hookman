import { Injectable } from '@angular/core';

declare var $: any;

@Injectable()
export class NotificationService {
    public showInfo(message: string) {
        $.notify({
            icon: '',
            message
        },
        {
            type: 'info',
            timer: 4000,
            placement: {
                from: 'top',
                align: 'center'
            }
        });
    }

    public showSuccess(message: string) {
        $.notify({
            icon: '',
            message
        },
        {
            type: 'success',
            timer: 4000,
            placement: {
                from: 'top',
                align: 'center'
            }
        });
    }

    public showWarning(message: string) {
        $.notify({
            icon: '',
            message
        },
        {
            type: 'warning',
            timer: 4000,
            placement: {
                from: 'top',
                align: 'center'
            }
        });
    }

    public showError(message: string) {
        $.notify({
            icon: '',
            message
        },
        {
            type: 'danger',
            timer: 4000,
            placement: {
                from: 'top',
                align: 'center'
            }
        });
    }
}
