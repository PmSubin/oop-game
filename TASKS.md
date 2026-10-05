# Chia task

Mỗi phần là một **Issue** trên GitHub, được gán (assign) cho đúng người làm.
Thầy xem được ai làm gì ở 3 chỗ:

1. Tab **Issues**: mỗi phần gán cho ai.
2. Tab **Pull requests**: code của từng người gửi lên, ai gửi, khi nào.
3. **Insights > Contributors**: số commit và số dòng code của từng người.

Phần 1, 2, 3 **làm song song được**, không ai phải chờ ai. Phần 4 ghép 3 phần kia lại.

## Quy định chung (áp dụng cho mọi phần)

**Đã có sẵn, KHÔNG được sửa** (cả nhóm cùng dùng):

| File | Là gì |
| --- | --- |
| `src/OopGame.Core/Menu/Dish.cs` | Lớp cha trừu tượng của mọi món: `Name`, `Price`, `CookTimeSeconds`, `abstract IReadOnlyDictionary<string, int> GetIngredients()` |
| `src/OopGame.Core/IUpdatable.cs` | `interface IUpdatable { void Update(int elapsedSeconds); }`. Quán gọi `Update(1)` mỗi giây |
| `src/OopGame.Core/Ingredients.cs` | Tên nguyên liệu dùng chung: `Ingredients.Beef`, `Ingredients.Pork`... và `Ingredients.All` |

**Quy tắc code:**

- C# .NET 9, project `OopGame.Core` (đã bật `Nullable` và `ImplicitUsings`). Không thêm thư viện NuGet nào.
- Chỉ tạo và sửa file **trong thư mục của phần mình**. Không sửa `MainForm`, không sửa file của phần khác.
- Mỗi lớp một file, tên file trùng tên lớp. Dùng file-scoped namespace (`namespace OopGame.Core.Menu;`).
- Tên lớp, phương thức, thuộc tính **đúng chính xác** như mô tả bên dưới, vì phần 4 sẽ gọi đúng các tên đó.
- Tên nguyên liệu luôn dùng hằng số trong `Ingredients`, không gõ tay chuỗi.
- Trường private đặt tên `_camelCase`, thuộc tính và phương thức `PascalCase`. Comment bằng tiếng Việt.
- Trước khi gửi Pull request: build toàn bộ solution phải **0 lỗi**.
- Nếu dùng AI để viết: phải **đọc hiểu từng dòng**, vì thầy có thể hỏi vấn đáp. Commit bằng tài khoản GitHub của chính mình.

---

## Phần 1. Thực đơn và món ăn

**Thư mục:** `src/OopGame.Core/Menu/` **Namespace:** `OopGame.Core.Menu`
**OOP thể hiện:** kế thừa nhiều cấp (`Dish` → `MainDish` → `Pho`), đa hình (`GetIngredients()`, `ToString()`), đóng gói (`MenuBook`).

### 1.1. Hai lớp cha trung gian

- `public abstract class MainDish : Dish` (món chính)
  - Constructor `protected MainDish(string name, int price, int cookTimeSeconds)` gọi `base(...)`.
  - Override `ToString()` trả về `"[Món chính] " + base.ToString()`, ví dụ `[Món chính] Phở bò - 45.000đ`.
- `public abstract class Drink : Dish` (đồ uống)
  - Constructor tương tự.
  - Override `ToString()` trả về `"[Đồ uống] " + base.ToString()`.

### 1.2. Năm món cụ thể

Mỗi món là một lớp `public class`, constructor **không tham số**, gọi `base(tên, giá, thời gian nấu)`.
`GetIngredients()` trả về một `Dictionary<string, int>` với **khoá là hằng số trong `Ingredients`**, giá trị là số lượng.

| Lớp | Kế thừa | Name | Price | CookTimeSeconds | Nguyên liệu (mỗi thứ 1 phần) |
| --- | --- | --- | --- | --- | --- |
| `Pho` | `MainDish` | `"Phở bò"` | 45000 | 6 | `RiceNoodle`, `Beef`, `Vegetables` |
| `BunBo` | `MainDish` | `"Bún bò"` | 45000 | 6 | `Vermicelli`, `Beef`, `Vegetables` |
| `ComTam` | `MainDish` | `"Cơm tấm"` | 40000 | 5 | `Rice`, `Pork`, `Egg` |
| `BanhMi` | `MainDish` | `"Bánh mì"` | 25000 | 3 | `Bread`, `Pork`, `Vegetables` |
| `TraDa` | `Drink` | `"Trà đá"` | 5000 | 1 | `Tea`, `Ice` |

Gợi ý: lưu công thức trong một trường `private static readonly Dictionary<string, int>` rồi trả về nó, để không tạo dictionary mới mỗi lần gọi.

### 1.3. Lớp `MenuBook` (thực đơn)

```csharp
public class MenuBook
{
    // private readonly List<Dish> _dishes;  danh sách món, không cho bên ngoài sửa
    public MenuBook()                            // tạo sẵn đủ 5 món ở bảng trên
    public int Count { get; }                    // số món
    public IReadOnlyList<Dish> GetAll()          // trả về danh sách chỉ đọc
    public Dish GetRandom(Random random)         // món ngẫu nhiên, dùng random được truyền vào
    public Dish? FindByName(string name)         // tìm theo tên, không phân biệt hoa thường; không có thì null
}
```

**Kiểm tra xong khi:** `new MenuBook().Count == 5`; `FindByName("phở bò")` trả về món Phở; `new Pho().GetIngredients()[Ingredients.Beef] == 1`; build 0 lỗi.

---

## Phần 2. Khách hàng

**Thư mục:** `src/OopGame.Core/Customers/` **Namespace:** `OopGame.Core.Customers`
**OOP thể hiện:** lớp trừu tượng, cài đặt interface `IUpdatable`, đa hình (`CalculateTip()`, `ReducePatience()`), đóng gói (`Patience`).

### 2.1. Lớp `public abstract class Customer : IUpdatable`

```csharp
public string Name { get; }
public Dish Order { get; }                  // món khách gọi (kiểu Dish trong OopGame.Core.Menu)
public int MaxPatience { get; }             // số giây kiên nhẫn tối đa
public int Patience { get; private set; }   // luôn trong khoảng 0..MaxPatience (dùng Math.Clamp)
public int WaitedSeconds { get; private set; }  // đã chờ bao lâu
public bool IsLeaving => Patience == 0;     // hết kiên nhẫn thì bỏ về
public abstract string TypeName { get; }    // "Khách thường", "Khách VIP", "Khách khó tính"

protected Customer(string name, Dish order, int maxPatience)  // Patience bắt đầu = maxPatience

public void Update(int elapsedSeconds)
// elapsedSeconds <= 0 thì không làm gì.
// Ngược lại: WaitedSeconds += elapsedSeconds; rồi gọi ReducePatience(elapsedSeconds).

protected virtual void ReducePatience(int seconds)   // mặc định: Patience giảm đúng seconds
protected void SetPatience(int value)                // để lớp con đổi Patience (tự kẹp 0..MaxPatience)

public abstract int CalculateTip();   // tiền tip khi được phục vụ, dựa vào WaitedSeconds và Order.Price

public override string ToString()
// Ví dụ: "Anh Minh (Khách VIP) gọi Phở bò - kiên nhẫn 20/25"
```

### 2.2. Ba loại khách

Tiền tip tính bằng số nguyên: `Order.Price * phần_trăm / 100`.

| Lớp | TypeName | MaxPatience | Patience giảm thế nào | Tip |
| --- | --- | --- | --- | --- |
| `NormalCustomer` | `"Khách thường"` | 40 | Mặc định (1 giây giảm 1) | 10% nếu `WaitedSeconds <= 20`, ngược lại 0 |
| `VipCustomer` | `"Khách VIP"` | 25 | Mặc định | 30% nếu `WaitedSeconds <= 15`, ngược lại 10% |
| `PickyCustomer` | `"Khách khó tính"` | 40 | Override: khi `WaitedSeconds > 10` thì giảm **gấp đôi** | 20% nếu `WaitedSeconds <= 10`, ngược lại 0 |

Mỗi lớp có constructor `public Xxx(string name, Dish order)`, tự truyền `MaxPatience` của mình vào `base`.

### 2.3. Lớp `CustomerFactory`

```csharp
public class CustomerFactory
{
    public CustomerFactory(Random random)
    public Customer CreateRandom(IReadOnlyList<Dish> menu)
    // menu rỗng thì ném ArgumentException.
    // Chọn món ngẫu nhiên trong menu, chọn tên ngẫu nhiên trong danh sách ít nhất 8 tên tiếng Việt
    // (ví dụ "Anh Minh", "Chị Lan", "Cô Hoa", "Bác Tư"...).
    // Tỉ lệ loại khách: 60% NormalCustomer, 20% VipCustomer, 20% PickyCustomer.
}
```

Chỉ dùng `Dish` và `IReadOnlyList<Dish>`, **không dùng `MenuBook`**, để không phải chờ phần 1.
Khi chưa có món thật của phần 1, có thể tự tạo một lớp món tạm **trong project test riêng hoặc trong code thử, không commit**.

**Kiểm tra xong khi:** một `VipCustomer` sau `Update(1)` 25 lần thì `IsLeaving == true`; một `PickyCustomer` sau 15 lần `Update(1)` có `Patience == 20` (10 giây đầu giảm 10, 5 giây sau giảm 10); build 0 lỗi.

---

## Phần 3. Bếp và kho nguyên liệu

**Thư mục:** `src/OopGame.Core/Cooking/` **Namespace:** `OopGame.Core.Cooking`
(Không đặt namespace là `Kitchen` vì sẽ trùng tên lớp `Kitchen` và C# báo lỗi.)
**OOP thể hiện:** đóng gói (kho, bảng giá), cài đặt interface `IUpdatable`, phối hợp nhiều đối tượng (`Kitchen` dùng `Inventory`).

### 3.1. Lớp `Inventory` (kho)

```csharp
public class Inventory
{
    // private readonly Dictionary<string, int> _stock;
    public int GetAmount(string ingredient)          // chưa có thì 0
    public void Add(string ingredient, int amount)   // amount <= 0 thì ném ArgumentOutOfRangeException
    public bool HasIngredients(Dish dish)            // đủ mọi nguyên liệu trong dish.GetIngredients() không
    public bool TryConsume(Dish dish)                // đủ thì trừ hết và trả true; thiếu thì KHÔNG trừ gì, trả false
    public IReadOnlyDictionary<string, int> GetAll() // trả về bản chỉ đọc
}
```

### 3.2. Lớp `Supplier` (nhà cung cấp)

```csharp
public class Supplier
{
    public Supplier()                                  // tạo sẵn bảng giá bên dưới
    public int GetPrice(string ingredient)             // không bán thì ném ArgumentException
    public int GetTotalPrice(string ingredient, int amount)   // GetPrice * amount
    public IReadOnlyList<string> GetAvailableIngredients()
}
```

Bảng giá (đồng cho 1 phần), dùng hằng số trong `Ingredients`:

| RiceNoodle | Vermicelli | Beef | Pork | Rice | Bread | Egg | Vegetables | Tea | Ice |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 5000 | 5000 | 15000 | 10000 | 3000 | 4000 | 3000 | 2000 | 1000 | 500 |

### 3.3. Lớp `CookingOrder` (một món đang nấu)

```csharp
public class CookingOrder
{
    public CookingOrder(Dish dish)                  // RemainingSeconds bắt đầu = dish.CookTimeSeconds
    public Dish Dish { get; }
    public int RemainingSeconds { get; private set; }
    public bool IsDone => RemainingSeconds == 0;
    public void Advance(int seconds)                // giảm RemainingSeconds, không xuống dưới 0
    public override string ToString()               // ví dụ "Phở bò - còn 3 giây"
}
```

### 3.4. Lớp `public class Kitchen : IUpdatable` (bếp)

```csharp
public const int MaxSlots = 2;                          // nấu tối đa 2 món cùng lúc
public Kitchen(Inventory inventory)
public IReadOnlyList<CookingOrder> CookingOrders { get; }   // đang nấu
public IReadOnlyList<Dish> ReadyDishes { get; }             // đã xong, chờ mang ra
public bool IsFull { get; }                                 // CookingOrders.Count >= MaxSlots

public bool StartCooking(Dish dish)
// Bếp đầy thì trả false. Gọi inventory.TryConsume(dish); thiếu nguyên liệu thì trả false.
// Đủ thì thêm new CookingOrder(dish) vào danh sách đang nấu, trả true.

public void Update(int elapsedSeconds)
// Gọi Advance cho mọi món đang nấu; món nào IsDone thì chuyển từ CookingOrders sang ReadyDishes.

public bool TakeReadyDish(Dish dish)
// Tìm trong ReadyDishes món có cùng Name với dish; có thì xoá khỏi danh sách và trả true, không có thì false.
```

**Kiểm tra xong khi:** kho rỗng thì `StartCooking` trả `false`; thêm đủ nguyên liệu, nấu một món 3 giây, gọi `Update(1)` 3 lần thì món nằm trong `ReadyDishes`; nấu món thứ 3 khi đang nấu 2 món thì trả `false`; build 0 lỗi.

---

## Phần 4. Quán, ngày làm việc và giao diện

**Thư mục:** `src/OopGame.Core/Game/` và `src/OopGame.WinForms/`

- Lớp `Restaurant`: giữ tiền, uy tín, ngày, giờ, `MenuBook`, `CustomerFactory`, danh sách khách đang chờ, `Kitchen`, `Inventory`, `Supplier`.
  - `OpenDay()`, `Tick()` (gọi mỗi giây: tăng giờ, thỉnh thoảng thêm khách, gọi `Update(1)` của khách và bếp, khách `IsLeaving` thì xoá và trừ uy tín).
  - `bool Serve(Customer customer)`: `kitchen.TakeReadyDish(customer.Order)` thành công thì cộng `Order.Price + CalculateTip()`.
  - `bool BuyIngredient(string ingredient, int amount)`: đủ tiền thì trừ tiền và `inventory.Add`.
  - Tiền và uy tín để `private set`.
- Lớp `DaySummary`: số khách đã phục vụ, số khách bỏ về, tiền kiếm được trong ngày.
- Giao diện: `System.Windows.Forms.Timer` gọi `Tick()` mỗi giây; nối 4 nút; form Nhập hàng; hộp thoại tổng kết cuối ngày.

---

## Bảng phân công

| Phần | Người làm | Issue |
| --- | --- | --- |
| 1. Thực đơn và món ăn | (điền tên) | #1 |
| 2. Khách hàng | (điền tên) | #2 |
| 3. Bếp và kho nguyên liệu | (điền tên) | #3 |
| 4. Quán, ngày làm việc và giao diện | (điền tên) | #4 |

Làm xong phần chính, ai còn thời gian thì nhận thêm: hình ảnh món ăn, âm thanh, lưu tiến trình, nâng cấp quán (mua thêm bếp), sự kiện ngẫu nhiên (giờ cao điểm, khách nổi tiếng).
