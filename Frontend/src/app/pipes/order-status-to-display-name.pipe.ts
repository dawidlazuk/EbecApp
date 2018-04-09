import { Pipe, PipeTransform } from '@angular/core';
import { OrderStatus } from '../order-status.enum';

@Pipe({
  name: 'orderStatusToDisplayName'
})
export class OrderStatusToDisplayNamePipe implements PipeTransform {

  transform(value: OrderStatus, args?: any): string {
    switch(value){
      case OrderStatus.new:
        return "New";

      case OrderStatus.inProgress:
        return "In progress";

      case OrderStatus.readyToReceive:
        return "Ready to receive";

      case OrderStatus.finished:
        return "Finished";

      case OrderStatus.cancelled:
        return "Cancelled";

      case OrderStatus.cancelledByOrganisers:
        return "Cancelled by organisers";        
    }
  }

}
