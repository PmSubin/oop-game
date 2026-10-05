# Chia task

Mỗi phần là một **Issue** trên GitHub, được gán (assign) cho đúng người làm.
Thầy xem được ai làm gì ở 3 chỗ:

1. Tab **Issues**: mỗi phần gán cho ai.
2. Tab **Pull requests**: code của từng người gửi lên, ai gửi, khi nào.
3. **Insights > Contributors**: số commit và số dòng code của từng người.

Lớp `Dish` (trong `src/OopGame.Core/Menu/Dish.cs`) đã có sẵn, là nền chung mà phần nào cũng dùng.
**Không sửa tên hay xoá phương thức trong `Dish`** mà chưa báo cả nhóm.

Interface dùng chung, ai làm phần 2 tạo trước tiên vì phần 3 và 4 cũng dùng:

```csharp
// src/OopGame.Core/IUpdatable.cs
public interface IUpdatable
{
    void Update(int elapsedSeconds);   // gọi mỗi giây khi quán đang mở
}
```

---

## Phần 1. Thực đơn và món ăn

Thư mục: `src/OopGame.Core/Menu/`

- Ít nhất 5 món kế thừa `Dish`, mỗi món một giá, thời gian nấu và công thức khác nhau.
  Ví dụ: `Pho`, `BanhMi`, `ComTam`, `BunBo`, `TraDa`.
- Món nước và món khô có thể tách thêm một tầng lớp cha (`Drink`, `MainDish`) cho rõ kế thừa nhiều cấp.
- Lớp `MenuBook` (thực đơn): danh sách món để `private`, có `IReadOnlyList<Dish> GetAll()`,
  `Dish GetRandom()` (để khách gọi món ngẫu nhiên), `Dish? FindByName(string name)`.

## Phần 2. Khách hàng

Thư mục: `src/OopGame.Core/Customers/`

- Tạo `IUpdatable` ở trên.
- Lớp `abstract Customer : IUpdatable`, có:
  - `Name`, `Order` (món đã gọi, kiểu `Dish`), `Patience`, `MaxPatience` (đóng gói, chỉ đọc từ ngoài).
  - `bool IsLeaving`: hết kiên nhẫn thì bỏ về.
  - `virtual void ReducePatience(int seconds)` và `Update()` gọi nó.
  - `abstract int CalculateTip(int waitedSeconds)`: tiền tip khi được phục vụ.
- 3 lớp con, mỗi loại cư xử khác nhau:
  - `NormalCustomer`: bình thường.
  - `VipCustomer`: trả tip cao, nhưng kiên nhẫn ít.
  - `PickyCustomer`: chỉ tip nếu được phục vụ nhanh, chờ lâu thì kiên nhẫn giảm gấp đôi.
- Lớp `CustomerFactory` có `Customer CreateRandom(MenuBook menu)`: tạo khách ngẫu nhiên gọi món ngẫu nhiên.

## Phần 3. Bếp và kho nguyên liệu

Thư mục: `src/OopGame.Core/Kitchen/`

- Lớp `Inventory` (kho): số lượng nguyên liệu để `private`, có
  `bool HasIngredients(Dish dish)`, `void Consume(Dish dish)`, `void Add(string ingredient, int amount)`, `int GetAmount(string ingredient)`.
- Lớp `Supplier` (nhà cung cấp): bảng giá nguyên liệu, `int GetPrice(string ingredient)`.
- Lớp `Kitchen : IUpdatable`: nấu được tối đa 2 món cùng lúc.
  - `bool StartCooking(Dish dish)`: thiếu nguyên liệu hoặc bếp bận thì trả về `false`.
  - `Update()` đếm thời gian, món nào xong thì chuyển sang danh sách món đã xong.
  - `IReadOnlyList<Dish> ReadyDishes`, `bool TakeReadyDish(Dish dish)`.

## Phần 4. Quán, ngày làm việc và giao diện

Thư mục: `src/OopGame.Core/Game/` và `src/OopGame.WinForms/`

- Lớp `Restaurant`: giữ tiền, uy tín, ngày, giờ, danh sách khách đang chờ, bếp, kho.
  - `OpenDay()`, `Tick()` (gọi mỗi giây: tăng giờ, thỉnh thoảng thêm khách, gọi `Update()` của khách và bếp, khách bỏ về thì trừ uy tín).
  - `bool Serve(Customer customer)`: đúng món thì cộng tiền + tip.
  - `bool BuyIngredient(string ingredient, int amount)`: đủ tiền thì mua.
  - Tiền và uy tín để `private set`, không cho sửa thẳng từ ngoài.
- Lớp `DaySummary`: số khách đã phục vụ, số khách bỏ về, tiền kiếm được trong ngày.
- Giao diện: dùng `System.Windows.Forms.Timer` gọi `Tick()` mỗi giây; nối 4 nút với `Restaurant`;
  cập nhật tiền, uy tín, giờ, 3 danh sách và log; form **Nhập hàng** riêng; hộp thoại tổng kết cuối ngày.
- Phần này dùng code của phần 1, 2, 3. Trong lúc chờ, có thể tạo lớp tạm để thử giao diện rồi thay sau.

---

## Bảng phân công

| Phần | Người làm | Issue |
| --- | --- | --- |
| 1. Thực đơn và món ăn | (điền tên) | #1 |
| 2. Khách hàng | (điền tên) | #2 |
| 3. Bếp và kho nguyên liệu | (điền tên) | #3 |
| 4. Quán, ngày làm việc và giao diện | (điền tên) | #4 |

Làm xong phần chính, ai còn thời gian thì nhận thêm: hình ảnh món ăn, âm thanh, lưu tiến trình, nâng cấp quán (mua thêm bếp), sự kiện ngẫu nhiên (giờ cao điểm, khách nổi tiếng).
