namespace api.Models.OrderAggregate
{
    //สถานะการชำระเงิน
    public enum OrderStatus //enum ชนิดข้อมูลอย่างหนึ่งที่เป็นตัวเลข
    {
        Pending,
        PaymentReceived,
        PaymentFailed
    }
}
