import { OrderStatusToDisplayNamePipe } from './order-status-to-display-name.pipe';

describe('OrderStatusToDisplayNamePipe', () => {
  it('create an instance', () => {
    const pipe = new OrderStatusToDisplayNamePipe();
    expect(pipe).toBeTruthy();
  });
});
