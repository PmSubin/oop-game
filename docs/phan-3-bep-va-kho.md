# Phần 3: Bếp và kho nguyên liệu

| Issue | Nhánh | Độ khó | Thời gian ước tính |
| --- | --- | --- | --- |
| [#3](https://github.com/PmSubin/oop-game/issues/3) | `phan-3-bep-va-kho` | Khó nhất trong 3 phần đầu | 4 đến 6 giờ |

## A. Bạn làm gì (đọc trước)

Bạn làm **nhà bếp** của quán, gồm 4 lớp:

- **`Inventory` (kho):** quán còn bao nhiêu thịt bò, rau, bánh phở... Có thể thêm nguyên liệu, kiểm tra đủ nấu một món không, và trừ nguyên liệu khi nấu.
  Quan trọng: **thiếu một thứ là không trừ gì cả** (không được trừ dở dang).
- **`Supplier` (nhà cung cấp):** bảng giá mua từng nguyên liệu.
- **`CookingOrder`:** một món đang nấu, còn bao nhiêu giây nữa thì xong.
- **`Kitchen` (bếp):**
  - Nấu **tối đa 2 món cùng lúc**.
  - Bếp đầy hoặc thiếu nguyên liệu thì không nấu được.
  - Mỗi giây được gọi `Update(1)` để đếm ngược. Món nào xong thì chuyển sang danh sách "đã nấu xong".

**OOP bạn thể hiện được:**

- **Đóng gói:** kho và bảng giá để `private`, bên ngoài chỉ thao tác qua phương thức. Không ai sửa thẳng số lượng được.
- **Interface:** `Kitchen` cài đặt `IUpdatable`.
- **Kết hợp đối tượng (composition):** `Kitchen` dùng `Inventory`, quản lý nhiều `CookingOrder`.
- **Đa hình qua lớp cha:** `Kitchen` làm việc với mọi món qua kiểu `Dish`, không cần biết là Phở hay Bánh mì.

## B. Các file bạn sẽ tạo

Tất cả nằm trong thư mục mới `src/OopGame.Core/Cooking/`.
Thư mục **không** đặt tên `Kitchen`, vì sẽ trùng tên lớp `Kitchen` và C# báo lỗi.

`Inventory.cs`, `Supplier.cs`, `CookingOrder.cs`, `Kitchen.cs`

Và di chuyển file test `Part3CookingTests.cs` (Bước 3).

## C. Các bước làm (làm đúng thứ tự)

### Bước 0. Chuẩn bị máy (chỉ làm 1 lần, ai làm rồi thì bỏ qua)

1. Có tài khoản GitHub và đã gửi **username** cho nhóm trưởng.
2. **Chấp nhận lời mời vào repo:** mở email GitHub gửi tới, bấm **View invitation** rồi **Accept invitation**.
   Không thấy email thì vào thẳng: https://github.com/PmSubin/oop-game/invitations
3. **Cài Visual Studio 2022 Community** (miễn phí): https://visualstudio.microsoft.com/vs/community/
   Lúc cài, tích chọn **.NET desktop development**.
   Máy đã có Visual Studio 2022: mở **Visual Studio Installer**, bấm **Update** để lên bản mới nhất (cần bản 17.12 trở lên mới chạy được .NET 9).
4. **Cài GitHub Desktop**: https://desktop.github.com rồi đăng nhập bằng tài khoản GitHub của **chính bạn**.
5. **Tải code về máy:** GitHub Desktop, chọn **File > Clone repository**, tab **GitHub.com**, chọn `PmSubin/oop-game`, chọn chỗ lưu, bấm **Clone**.
6. **Mở thử:** GitHub Desktop, chọn **Repository > Show in Explorer**, mở file `OopGame.sln`.
   Visual Studio mở lên thì bấm **F5**. Thấy cửa sổ "Quán Ăn Bận Rộn" có 4 nút là máy đã sẵn sàng.

### Bước 1. Lấy code mới nhất

Trong GitHub Desktop: ô **Current branch** chọn `main`, bấm **Fetch origin**. Nếu nút đổi thành **Pull origin** thì bấm tiếp.

### Bước 2. Tạo nhánh riêng cho phần của bạn

Bấm **Current branch > New branch**, gõ tên: `phan-3-bep-va-kho`, bấm **Create branch**, rồi bấm **Publish branch**.

Kiểm tra ô **Current branch** đang ghi `phan-3-bep-va-kho`. Từ giờ mọi thứ bạn làm nằm trên nhánh này, không ảnh hưởng tới ai.

### Bước 3. Bật bài kiểm tra của phần bạn

1. Trong thư mục code, vào `tests\OopGame.Tests\Pending\`.
2. **Cắt** (Ctrl+X) file `Part3CookingTests.cs`.
3. **Dán** (Ctrl+V) ra thư mục cha `tests\OopGame.Tests\`.

Lúc này build sẽ báo lỗi đỏ vì các lớp chưa có. Đó là bình thường, làm xong phần của bạn là hết lỗi.
**Chỉ di chuyển file của phần mình**, không đụng các file khác trong `Pending`.

### Bước 4. Nhờ AI viết code

1. Gửi cho AI (ChatGPT, Gemini, Claude...) **toàn bộ mục D. PROMPT CHO AI** ở cuối file này.
   Cách dễ nhất: gửi luôn cả file `.md` này cho AI, kèm câu: *"Làm đúng theo mục D. PROMPT CHO AI trong file này."*
2. AI trả về nhiều file code, mỗi file có ghi đường dẫn.
3. Tạo từng file trong Visual Studio:
   - Ở **Solution Explorer** (khung bên phải), chuột phải vào thư mục `Cooking` trong project `OopGame.Core`.
     Chưa có thư mục thì chuột phải vào project `OopGame.Core`, chọn **Add > New Folder**, đặt tên `Cooking`.
   - Chọn **Add > Class...**, gõ **đúng tên file** AI đưa, bấm **Add**.
   - Xoá hết nội dung Visual Studio tự sinh, dán code của AI vào, bấm Ctrl+S.
4. **Đọc hiểu từng file.** Chỗ nào chưa hiểu thì hỏi lại AI: *"Giải thích đoạn này cho người mới học"*. Thầy có thể hỏi vấn đáp.

### Bước 5. Build và chạy kiểm tra

1. Chọn **Build > Build Solution** (Ctrl+Shift+B). Khung **Error List** phải không còn lỗi (Errors = 0).
   Còn lỗi: copy nguyên văn dòng lỗi gửi AI, kèm câu *"Build bị lỗi này, sửa giúp"*.
2. Chọn **Test > Test Explorer**, bấm **Run All Tests** (nút hai tam giác xanh).
   Mọi bài trong `Part3CookingTests` phải có **dấu tích xanh**.
   Bài nào đỏ: bấm vào để xem thông báo, copy gửi AI kèm câu *"Bài kiểm tra này bị đỏ, hãy sửa code, không sửa file test"*.
3. **Tuyệt đối không sửa file test** để cho xanh. Nhóm trưởng sẽ chạy lại để kiểm tra.
4. Bấm **F5**, game vẫn mở lên bình thường là được.

### Bước 6. Commit (lưu lại), chia nhỏ từng việc

GitHub Desktop hiện danh sách file thay đổi ở khung bên trái. Mỗi lần commit:
tích chọn các file của việc đó (bỏ tích file khác), gõ ô **Summary**, bấm **Commit to phan-3-bep-va-kho**.

Nên chia như sau:

| Lần | Chọn các file | Summary ghi |
| --- | --- | --- |
| 1 | `Part3CookingTests.cs` (vừa di chuyển, sẽ hiện 2 dòng: xoá ở Pending, thêm ở ngoài) | `Bật bài kiểm tra Phần 3` |
| 2 | `Inventory.cs` | `Thêm kho nguyên liệu Inventory` |
| 3 | `Supplier.cs` | `Thêm nhà cung cấp Supplier` |
| 4 | `CookingOrder.cs`, `Kitchen.cs` | `Thêm bếp Kitchen và CookingOrder` |

Mỗi commit ghi rõ đã làm gì. Thầy xem lịch sử commit để biết bạn làm phần nào.

### Bước 7. Đẩy code lên GitHub

Bấm **Push origin** ở thanh trên cùng của GitHub Desktop.

### Bước 8. Tạo Pull request (nộp bài cho nhóm trưởng)

1. Trong GitHub Desktop bấm **Create Pull Request** (hoặc **Branch > Create pull request**). Trang GitHub mở ra.
2. Dòng trên cùng phải là: `base: main` ← `compare: phan-3-bep-va-kho`.
3. Ô **Title** điền: `Phần 3: Bếp và kho nguyên liệu`
4. Ô mô tả, copy mẫu này rồi điền vào chỗ trống:

   ```
   Closes #3

   ## Đã làm
   - (liệt kê các lớp đã tạo)

   ## Kiểm tra
   - Build: 0 lỗi
   - Test Explorer: Part3CookingTests xanh hết (__/__ bài)
   - F5: game vẫn mở bình thường
   ```

5. Bấm **Create pull request**, rồi nhắn nhóm trưởng vào duyệt.

### Bước 9. Sửa theo góp ý (nếu có)

Nhóm trưởng góp ý ngay trong Pull request. Bạn sửa code **trên cùng nhánh** `phan-3-bep-va-kho`, commit, bấm **Push origin**.
Pull request tự cập nhật, không cần tạo cái mới.
Khi nhóm trưởng bấm **Merge** là xong. Issue #3 tự đóng, tên bạn nằm trong lịch sử của repo.

### Lỗi thường gặp

| Hiện tượng | Cách xử lý |
| --- | --- |
| Mở `OopGame.sln` báo không hỗ trợ .NET 9 hoặc lỗi `NETSDK1045` | Cập nhật Visual Studio 2022 qua Visual Studio Installer |
| Clone không thấy repo `oop-game` | Chưa bấm Accept lời mời (Bước 0) |
| Push bị từ chối, báo không có quyền | Chưa Accept lời mời, hoặc GitHub Desktop đang đăng nhập tài khoản khác |
| Lỡ sửa code khi đang ở nhánh `main` | Cứ tạo nhánh mới (Bước 2). GitHub Desktop hỏi thì chọn **Bring my changes to phan-3-bep-va-kho** |
| Test Explorer trống trơn | Build lại (Ctrl+Shift+B) rồi đợi vài giây |
| Báo **conflict** | Không xoá code của người khác, nhắn nhóm trưởng cùng xử lý |

## D. PROMPT CHO AI

> Copy **từ dòng "BẮT ĐẦU PROMPT" đến hết file** gửi cho AI, hoặc gửi cả file này cho AI.

==================== BẮT ĐẦU PROMPT ====================

Bạn là lập trình viên C# đang hướng dẫn một sinh viên năm nhất. Hãy viết code cho **Phần 3: Bếp và kho nguyên liệu** của đồ án môn Lập trình hướng đối tượng, **đúng chính xác** theo đặc tả bên dưới.

### D1. Bối cảnh dự án

- Game **"Quán Ăn Bận Rộn"**, viết bằng C# .NET 9 và Windows Forms. Người chơi điều hành một quán ăn: khách vào gọi món, người chơi nấu rồi phục vụ để nhận tiền và tip. Khách chờ lâu thì bỏ về, quán mất uy tín. Hết nguyên liệu thì nhập thêm.
- Solution `OopGame.sln` gồm 3 project:
  - `src/OopGame.Core`: class library `net9.0`, chứa **toàn bộ logic game**, không có giao diện (không dùng `System.Windows.Forms`).
  - `src/OopGame.WinForms`: giao diện Windows Forms (`net9.0-windows`), tham chiếu `OopGame.Core`.
  - `tests/OopGame.Tests`: xUnit, tham chiếu `OopGame.Core`.
- `OopGame.Core.csproj` đã bật `<Nullable>enable</Nullable>` và `<ImplicitUsings>enable</ImplicitUsings>`.
- Nhóm 4 người làm 4 phần **song song**:
  - Phần 1, Thực đơn: namespace `OopGame.Core.Menu`.
  - Phần 2, Khách hàng: namespace `OopGame.Core.Customers`.
  - Phần 3, Bếp và kho: namespace `OopGame.Core.Cooking`.
  - Phần 4, Quán và giao diện: namespace `OopGame.Core.Game` và project WinForms.
- Phần này chỉ dựa vào các file dùng chung ở mục D3 (dùng kiểu `Dish` và hằng số `Ingredients`). KHÔNG dùng `MenuBook` hay các món cụ thể của Phần 1, vì Phần 1 đang được làm song song.

### D2. Yêu cầu bắt buộc

1. **Đúng chính xác** namespace, tên lớp, tên và kiểu của thuộc tính, phương thức, tham số, giá trị trả về như đặc tả. Không đổi tên, không thêm hay bớt tham số. Được thêm thành viên `private` nếu cần.
2. Chỉ tạo file trong `src/OopGame.Core/Cooking/`. **Không sửa** bất kỳ file nào đã có (nhất là `Dish.cs`, `Ingredients.cs`, `IUpdatable.cs` và các file test). Không thêm thư viện NuGet.
3. Mỗi lớp một file, tên file trùng tên lớp. Dùng file-scoped namespace (`namespace OopGame.Core.Xxx;`).
4. Code phải **dễ hiểu với sinh viên năm nhất** để trả lời vấn đáp. Áp dụng cho code bạn viết, không áp dụng cho file test có sẵn:
   - Dùng `for`, `foreach`, `if` rõ ràng.
   - **Không dùng LINQ** (`Where`, `Select`, `Any`, `All`, `FirstOrDefault`, `ToList`...), không dùng lambda, không dùng `var`, không dùng `record`.
   - Phương thức và constructor viết thân bằng `{ }` đầy đủ.
   - Chỉ được dùng `=>` cho **thuộc tính chỉ đọc một dòng**, ví dụ `public bool IsLeaving => Patience == 0;`, `public int Count => _dishes.Count;`, `public override string TypeName => "Khách VIP";`.
   - Trong đặc tả, dòng nào chỉ ghi chữ ký (không có thân) thì bạn tự viết thân theo comment bên cạnh. Constructor của lớp con gọi lớp cha bằng `: base(...)`.
5. Mỗi lớp và mỗi thành viên `public`, `protected` hoặc `override` có comment `/// <summary>` tiếng Việt, ngắn gọn. Trường private (kể cả `static`) đặt tên `_camelCase`.
6. **Đóng gói:** dữ liệu bên trong (`List`, `Dictionary`, mảng) luôn là trường `private readonly`.
   Trả ra ngoài bằng bản chỉ đọc: `List` thì dùng `.AsReadOnly()`, `Dictionary` thì dùng `new ReadOnlyDictionary<TKey, TValue>(...)`.
   Khi dùng `ReadOnlyDictionary` phải thêm `using System.Collections.ObjectModel;` ở đầu file (không có sẵn trong ImplicitUsings).
   Không bao giờ trả thẳng `List` hay `Dictionary` bên trong ra ngoài.
7. Kiểm tra tham số không hợp lệ và ném **đúng loại exception** **chỉ ở những chỗ đặc tả ghi rõ**. Không tự thêm kiểm tra hay exception khác.
8. Code phải qua **toàn bộ** bài kiểm tra xUnit ở mục D5. Không được đề nghị sửa bài kiểm tra.

### D3. Code có sẵn trong repo (chỉ dùng, không sửa)

`src/OopGame.Core/Menu/Dish.cs`

```csharp
namespace OopGame.Core.Menu;

/// <summary>
/// Lớp cha trừu tượng cho mọi món ăn trong quán.
/// Không tạo trực tiếp được, phải tạo qua lớp con (Pho, BanhMi, ComTam...).
/// </summary>
public abstract class Dish
{
    public string Name { get; }
    public int Price { get; }
    public int CookTimeSeconds { get; }

    protected Dish(string name, int price, int cookTimeSeconds)
    {
        if (price <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Giá món phải lớn hơn 0.");
        }
        if (cookTimeSeconds <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cookTimeSeconds), "Thời gian nấu phải lớn hơn 0.");
        }

        Name = name;
        Price = price;
        CookTimeSeconds = cookTimeSeconds;
    }

    /// <summary>
    /// Nguyên liệu cần để nấu một phần: tên nguyên liệu và số lượng.
    /// Mỗi món con tự khai báo công thức của mình.
    /// </summary>
    public abstract IReadOnlyDictionary<string, int> GetIngredients();

    // Giá luôn hiện kiểu Việt Nam (45.000đ), không phụ thuộc cài đặt ngôn ngữ của máy.
    public override string ToString()
    {
        return $"{Name} - {Price.ToString("N0", VietnameseCulture)}đ";
    }

    private static readonly System.Globalization.CultureInfo VietnameseCulture =
        System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
}
```

`src/OopGame.Core/Ingredients.cs`

```csharp
namespace OopGame.Core;

/// <summary>
/// Tên các nguyên liệu dùng chung cho cả nhóm.
/// Luôn dùng các hằng số này, không gõ tay chuỗi "Thịt bò" để tránh sai chính tả giữa các phần.
/// </summary>
public static class Ingredients
{
    public const string RiceNoodle = "Bánh phở";
    public const string Vermicelli = "Bún";
    public const string Beef = "Thịt bò";
    public const string Pork = "Thịt heo";
    public const string Rice = "Cơm";
    public const string Bread = "Bánh mì";
    public const string Egg = "Trứng";
    public const string Vegetables = "Rau";
    public const string Tea = "Trà";
    public const string Ice = "Đá";

    public static IReadOnlyList<string> All { get; } = new[]
    {
        RiceNoodle, Vermicelli, Beef, Pork, Rice, Bread, Egg, Vegetables, Tea, Ice
    };
}
```

`src/OopGame.Core/IUpdatable.cs`

```csharp
namespace OopGame.Core;

/// <summary>
/// Những thứ thay đổi theo thời gian khi quán đang mở (khách, bếp).
/// Quán gọi Update(1) mỗi giây.
/// </summary>
public interface IUpdatable
{
    void Update(int elapsedSeconds);
}
```

### D4. Đặc tả Phần 3

**Thư mục:** `src/OopGame.Core/Cooking/`. **Namespace:** `OopGame.Core.Cooking`.
Cần `using OopGame.Core.Menu;` để dùng `Dish`. `Ingredients` và `IUpdatable` nằm trong namespace `OopGame.Core` (namespace cha, tự thấy được).

#### Lớp `Inventory` (file `Inventory.cs`)

```csharp
public class Inventory
{
    private readonly Dictionary<string, int> _stock = new Dictionary<string, int>();

    public int GetAmount(string ingredient)
    // Số lượng hiện có. Nguyên liệu chưa từng thêm thì trả về 0 (dùng TryGetValue).

    public void Add(string ingredient, int amount)
    // amount <= 0 thì throw new ArgumentOutOfRangeException(nameof(amount), "Số lượng phải lớn hơn 0.").
    // Ngược lại cộng dồn vào số đang có.

    public bool HasIngredients(Dish dish)
    // Duyệt từng cặp bằng foreach (KeyValuePair<string, int> item in dish.GetIngredients()):
    // có thứ nào GetAmount(...) < số lượng cần thì trả về false. Đủ hết thì true.

    public bool TryConsume(Dish dish)
    // Nếu !HasIngredients(dish) thì trả về false và KHÔNG trừ gì cả.
    // Đủ thì trừ hết nguyên liệu của món rồi trả về true.

    public IReadOnlyDictionary<string, int> GetAll()
    // Trả về new ReadOnlyDictionary<string, int>(_stock).
}
```

#### Lớp `Supplier` (file `Supplier.cs`)

```csharp
public class Supplier
{
    private readonly Dictionary<string, int> _prices = new Dictionary<string, int>();   // giá 1 phần, đơn vị đồng

    public Supplier()
    // Thêm 10 dòng của bảng giá bên dưới vào _prices, theo đúng thứ tự trong bảng.
    // Khoá dùng hằng số trong Ingredients.

    public int GetPrice(string ingredient)
    // Nguyên liệu không có trong bảng giá thì
    // throw new ArgumentException("Nhà cung cấp không bán nguyên liệu này.", nameof(ingredient)).

    public int GetTotalPrice(string ingredient, int amount)
    // GetPrice(ingredient) * amount.

    public IReadOnlyList<string> GetAvailableIngredients()
    // Danh sách tên các nguyên liệu có bán (10 thứ), dạng chỉ đọc:
    // tạo List<string> từ các khoá của _prices rồi trả về .AsReadOnly().
}
```

Bảng giá (đồng cho 1 phần):

| Hằng số | Giá |
| --- | --- |
| `Ingredients.RiceNoodle` | 5000 |
| `Ingredients.Vermicelli` | 5000 |
| `Ingredients.Beef` | 15000 |
| `Ingredients.Pork` | 10000 |
| `Ingredients.Rice` | 3000 |
| `Ingredients.Bread` | 4000 |
| `Ingredients.Egg` | 3000 |
| `Ingredients.Vegetables` | 2000 |
| `Ingredients.Tea` | 1000 |
| `Ingredients.Ice` | 500 |

#### Lớp `CookingOrder` (file `CookingOrder.cs`)

```csharp
public class CookingOrder
{
    public CookingOrder(Dish dish)
    // Lưu dish. RemainingSeconds bắt đầu bằng dish.CookTimeSeconds.

    public Dish Dish { get; }
    public int RemainingSeconds { get; private set; }
    public bool IsDone => RemainingSeconds == 0;

    public void Advance(int seconds)
    // RemainingSeconds = Math.Max(0, RemainingSeconds - seconds). Không bao giờ xuống dưới 0.

    public override string ToString()
    // Định dạng: "{Dish.Name} - còn {RemainingSeconds} giây". Ví dụ: "Phở bò - còn 3 giây".
}
```

#### Lớp `Kitchen` (file `Kitchen.cs`)

```csharp
public class Kitchen : IUpdatable
{
    public const int MaxSlots = 2;                     // nấu tối đa 2 món cùng lúc

    private readonly Inventory _inventory;
    private readonly List<CookingOrder> _cookingOrders = new List<CookingOrder>();
    private readonly List<Dish> _readyDishes = new List<Dish>();

    public Kitchen(Inventory inventory)

    public IReadOnlyList<CookingOrder> CookingOrders { get; }   // trả về _cookingOrders.AsReadOnly()
    public IReadOnlyList<Dish> ReadyDishes { get; }             // trả về _readyDishes.AsReadOnly()
    public bool IsFull { get; }                                 // _cookingOrders.Count >= MaxSlots

    public bool StartCooking(Dish dish)
    // 1. Nếu IsFull thì trả về false (KHÔNG được trừ nguyên liệu).
    // 2. Nếu !_inventory.TryConsume(dish) thì trả về false.
    // 3. Thêm new CookingOrder(dish) vào _cookingOrders, trả về true.

    public void Update(int elapsedSeconds)
    // 1. Gọi Advance(elapsedSeconds) cho mọi món đang nấu.
    // 2. Món nào IsDone thì xoá khỏi _cookingOrders và thêm order.Dish vào CUỐI _readyDishes.
    //    Lưu ý: không được xoá phần tử khỏi List trong lúc đang foreach chính List đó.
    //    Cách làm: duyệt xuôi _cookingOrders, gom các món IsDone vào một List<CookingOrder> tạm;
    //    sau vòng lặp mới duyệt List tạm để xoá khỏi _cookingOrders và thêm vào _readyDishes.
    //    Nhờ vậy, món nào bắt đầu nấu trước thì đứng trước trong _readyDishes.

    public bool TakeReadyDish(Dish dish)
    // Tìm trong _readyDishes món đầu tiên có Name == dish.Name (so sánh theo TÊN, không so sánh đối tượng).
    // Có thì xoá món đó khỏi _readyDishes và trả về true. Không có thì trả về false.
}
```

### D5. Bài kiểm tra phải qua

File `tests/OopGame.Tests/Part3CookingTests.cs` (đã có sẵn, không được sửa):

```csharp
using OopGame.Core;
using OopGame.Core.Cooking;
using OopGame.Core.Menu;

namespace OopGame.Tests;

// Bài kiểm tra cho Phần 3: Bếp và kho nguyên liệu. Không sửa file này.
public class Part3CookingTests
{
    // Món giả, không phụ thuộc Phần 1: cần 1 thịt bò + 2 rau.
    private sealed class FakeDish : Dish
    {
        public FakeDish(string name = "Món thử", int cookTime = 3) : base(name, 30000, cookTime) { }

        public override IReadOnlyDictionary<string, int> GetIngredients()
        {
            return new Dictionary<string, int> { [Ingredients.Beef] = 1, [Ingredients.Vegetables] = 2 };
        }
    }

    private static Inventory FullInventory()
    {
        Inventory inventory = new Inventory();
        foreach (string ingredient in Ingredients.All)
        {
            inventory.Add(ingredient, 10);
        }
        return inventory;
    }

    [Fact]
    public void Inventory_StartsEmpty_AndAddAccumulates()
    {
        Inventory inventory = new Inventory();
        Assert.Equal(0, inventory.GetAmount(Ingredients.Beef));
        inventory.Add(Ingredients.Beef, 3);
        inventory.Add(Ingredients.Beef, 2);
        Assert.Equal(5, inventory.GetAmount(Ingredients.Beef));
        Assert.Equal(5, inventory.GetAll()[Ingredients.Beef]);
    }

    [Fact]
    public void Inventory_Add_RejectsZeroOrNegative()
    {
        Inventory inventory = new Inventory();
        Assert.Throws<ArgumentOutOfRangeException>(() => inventory.Add(Ingredients.Beef, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => inventory.Add(Ingredients.Beef, -1));
    }

    [Fact]
    public void Inventory_TryConsume_TakesAllOrNothing()
    {
        Inventory inventory = new Inventory();
        inventory.Add(Ingredients.Beef, 1);
        inventory.Add(Ingredients.Vegetables, 1);

        // Thiếu 1 rau: không được trừ gì cả.
        Assert.False(inventory.HasIngredients(new FakeDish()));
        Assert.False(inventory.TryConsume(new FakeDish()));
        Assert.Equal(1, inventory.GetAmount(Ingredients.Beef));
        Assert.Equal(1, inventory.GetAmount(Ingredients.Vegetables));

        inventory.Add(Ingredients.Vegetables, 1);
        Assert.True(inventory.TryConsume(new FakeDish()));
        Assert.Equal(0, inventory.GetAmount(Ingredients.Beef));
        Assert.Equal(0, inventory.GetAmount(Ingredients.Vegetables));
    }

    [Fact]
    public void Supplier_HasPriceTable()
    {
        Supplier supplier = new Supplier();
        Assert.Equal(5000, supplier.GetPrice(Ingredients.RiceNoodle));
        Assert.Equal(15000, supplier.GetPrice(Ingredients.Beef));
        Assert.Equal(10000, supplier.GetPrice(Ingredients.Pork));
        Assert.Equal(500, supplier.GetPrice(Ingredients.Ice));
        Assert.Equal(30000, supplier.GetTotalPrice(Ingredients.Pork, 3));
        Assert.Equal(10, supplier.GetAvailableIngredients().Count);
        Assert.Throws<ArgumentException>(() => supplier.GetPrice("Tôm hùm"));
    }

    [Fact]
    public void CookingOrder_CountsDownToZero()
    {
        CookingOrder order = new CookingOrder(new FakeDish(cookTime: 3));
        Assert.Equal(3, order.RemainingSeconds);
        Assert.Equal("Món thử - còn 3 giây", order.ToString());
        order.Advance(2);
        Assert.False(order.IsDone);
        order.Advance(5);
        Assert.Equal(0, order.RemainingSeconds);
        Assert.True(order.IsDone);
    }

    [Fact]
    public void Kitchen_ImplementsIUpdatable_AndHasTwoSlots()
    {
        Assert.True(typeof(IUpdatable).IsAssignableFrom(typeof(Kitchen)));
        Assert.Equal(2, Kitchen.MaxSlots);
    }

    [Fact]
    public void Kitchen_CannotCookWithoutIngredients()
    {
        Kitchen kitchen = new Kitchen(new Inventory());
        Assert.False(kitchen.StartCooking(new FakeDish()));
        Assert.Empty(kitchen.CookingOrders);
    }

    [Fact]
    public void Kitchen_CooksAtMostTwoDishes()
    {
        Kitchen kitchen = new Kitchen(FullInventory());
        Assert.True(kitchen.StartCooking(new FakeDish()));
        Assert.True(kitchen.StartCooking(new FakeDish()));
        Assert.True(kitchen.IsFull);
        Assert.False(kitchen.StartCooking(new FakeDish()));
        Assert.Equal(2, kitchen.CookingOrders.Count);
    }

    [Fact]
    public void Kitchen_WhenFull_DoesNotConsumeIngredients()
    {
        Inventory inventory = FullInventory();
        Kitchen kitchen = new Kitchen(inventory);
        kitchen.StartCooking(new FakeDish());
        kitchen.StartCooking(new FakeDish());
        kitchen.StartCooking(new FakeDish());
        Assert.Equal(8, inventory.GetAmount(Ingredients.Beef));
    }

    [Fact]
    public void Kitchen_MovesFinishedDishToReady()
    {
        Kitchen kitchen = new Kitchen(FullInventory());
        kitchen.StartCooking(new FakeDish("Món nhanh", 1));
        kitchen.StartCooking(new FakeDish("Món chậm", 3));

        kitchen.Update(1);
        Assert.Single(kitchen.ReadyDishes);
        Assert.Equal("Món nhanh", kitchen.ReadyDishes[0].Name);
        Assert.Single(kitchen.CookingOrders);
        Assert.False(kitchen.IsFull);

        kitchen.Update(1);
        kitchen.Update(1);
        Assert.Equal(2, kitchen.ReadyDishes.Count);
        Assert.Empty(kitchen.CookingOrders);
    }

    [Fact]
    public void Kitchen_TakeReadyDish_MatchesByName()
    {
        Kitchen kitchen = new Kitchen(FullInventory());
        kitchen.StartCooking(new FakeDish("Phở bò", 1));
        kitchen.Update(1);

        Assert.False(kitchen.TakeReadyDish(new FakeDish("Bánh mì")));
        Assert.True(kitchen.TakeReadyDish(new FakeDish("Phở bò")));
        Assert.Empty(kitchen.ReadyDishes);
        Assert.False(kitchen.TakeReadyDish(new FakeDish("Phở bò")));
    }

    [Fact]
    public void Kitchen_ListsAreReadOnly()
    {
        Kitchen kitchen = new Kitchen(FullInventory());
        Assert.False(kitchen.CookingOrders is List<CookingOrder>, "CookingOrders phải là danh sách chỉ đọc.");
        Assert.False(kitchen.ReadyDishes is List<Dish>, "ReadyDishes phải là danh sách chỉ đọc.");
    }
}
```

### D6. Cách trả lời

Trả lời theo đúng thứ tự sau:

1. Danh sách các file sẽ tạo, ghi đường dẫn đầy đủ.
2. Nội dung **đầy đủ** của từng file, mỗi file một khối code riêng, ghi đường dẫn ngay phía trên khối. Không viết tắt bằng "...", không bỏ sót phần nào.
3. Giải thích ngắn từng lớp: làm gì, thể hiện tính chất OOP nào (đóng gói, kế thừa, đa hình, trừu tượng), ở dòng nào.
4. Tự rà lại: đối chiếu từng bài kiểm tra ở mục D5 với code, nói rõ vì sao code qua bài đó.
5. Năm câu hỏi vấn đáp thầy có thể hỏi về phần này, kèm câu trả lời ngắn.

==================== HẾT PROMPT ====================
