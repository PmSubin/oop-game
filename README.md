# Quán Ăn Bận Rộn

Đồ án môn Lập trình hướng đối tượng. Game quản lý quán ăn viết bằng C# Windows Forms, nhóm 4 người.

## Game chơi thế nào

1. Bấm **Mở cửa** để bắt đầu một ngày. Đồng hồ chạy từ 08:00 đến 20:00.
2. Khách lần lượt vào quán, mỗi khách gọi một món và có thanh **kiên nhẫn** giảm dần.
3. Chọn món rồi bấm **Nấu món**. Nấu tốn nguyên liệu và mất vài giây.
4. Món nấu xong thì chọn khách, bấm **Phục vụ** để nhận tiền và tiền tip.
5. Khách chờ lâu quá sẽ bỏ về, quán bị giảm **uy tín**.
6. Hết nguyên liệu thì bấm **Nhập hàng** để mua thêm bằng tiền kiếm được.
7. Cuối ngày có bảng tổng kết. Uy tín về 0 là thua.

## OOP thể hiện ở đâu

| Tính chất | Ở đâu |
| --- | --- |
| Trừu tượng | `Dish`, `Customer` là lớp `abstract` |
| Kế thừa | `Pho`, `BanhMi`, `ComTam`... kế thừa `Dish`; `NormalCustomer`, `VipCustomer`, `PickyCustomer` kế thừa `Customer` |
| Đa hình | Mỗi loại khách override `CalculateTip()` và `ReducePatience()` theo cách riêng; mỗi món override `GetIngredients()` |
| Đóng gói | Tiền, uy tín, kho nguyên liệu để `private`, chỉ thay đổi qua phương thức |
| Interface | `IUpdatable` cho những thứ thay đổi theo từng giây (khách, bếp) |

## Cấu trúc thư mục

```
src/
  OopGame.Core/       Các lớp game (món ăn, khách, bếp, quán). Không có giao diện.
  OopGame.WinForms/   Giao diện: cửa sổ, nút bấm, danh sách.
```

Tách như vậy để phần logic OOP không phụ thuộc giao diện.

## Chạy game

Cần **Visual Studio 2022** (bản 17.12 trở lên) có cài workload **.NET desktop development**.

1. Mở file `OopGame.sln` bằng Visual Studio.
2. Bấm nút **Start** (tam giác xanh) hoặc phím `F5`.

## Làm việc nhóm

- Ai làm phần nào: xem [TASKS.md](TASKS.md).
- Hướng dẫn từng phần (kèm prompt cho AI): xem [docs/](docs/README.md).
- Chưa biết dùng GitHub: đọc [HUONG-DAN-GITHUB.md](HUONG-DAN-GITHUB.md).
