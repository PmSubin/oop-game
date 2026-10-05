# Phần 4: Quán, ngày làm việc và giao diện

| Issue | Nhánh | Độ khó | Thời gian ước tính |
| --- | --- | --- | --- |
| [#4](https://github.com/PmSubin/oop-game/issues/4) | `phan-4-quan-va-giao-dien` | Khó nhất (ghép cả game) | 6 đến 10 giờ |

## A. Bạn làm gì (đọc trước)

Bạn là **chủ quán**. Món của Phần 1, khách của Phần 2, bếp của Phần 3 đều về tay bạn, việc của bạn là **ghép tất cả thành game chơi được**.
Đồng hồ chạy, khách đại gia đang mất kiên nhẫn, reviewer khó tính đang lăm le viết review 1 sao...

Bạn làm:

- **`Restaurant` (quán):**
  - Giữ tiền, uy tín, ngày, đồng hồ, thực đơn, kho, bếp và danh sách khách đang chờ.
  - Mỗi giây gọi `Tick()` một lần: đồng hồ chạy 10 phút trong game, bếp nấu tiếp, khách mất kiên nhẫn, thỉnh thoảng có khách mới vào.
  - Một ngày từ 08:00 đến 20:00, tương đương 72 giây chơi thật.
- **`DaySummary`:** bảng tổng kết cuối ngày gồm số khách đã phục vụ, số khách bỏ về, doanh thu, chi phí, lợi nhuận.
- **Giao diện:**
  - Nối 4 nút có sẵn (Mở cửa, Nấu món, Phục vụ, Nhập hàng) với `Restaurant`.
  - Thêm form **Nhập hàng**.
  - Hiện hộp thoại tổng kết cuối ngày.

**Làm theo thứ tự:**

1. Đợi Phần 1, 2, 3 được merge vào `main`. Trong lúc chờ, có thể vẽ trước form Nhập hàng.
2. Bước 1 bên dưới (lấy code mới nhất) phải làm **sau khi** 3 phần kia đã merge.
3. Làm `DaySummary` và `Restaurant` trước, chạy kiểm tra xanh hết, rồi mới làm giao diện.

**OOP bạn thể hiện được:**

- **Đóng gói:** tiền, uy tín chỉ đổi qua `Serve`, `BuyIngredient`... Giao diện không sửa thẳng được.
- **Kết hợp đối tượng:** `Restaurant` chứa và điều phối `MenuBook`, `Kitchen`, `Inventory`, `Supplier`, `CustomerFactory`.
- **Đa hình:** `Restaurant` gọi `customer.Update(1)` và `customer.CalculateTip()` mà không cần biết loại khách.
- **Tách logic khỏi giao diện:** toàn bộ luật chơi nằm trong `OopGame.Core`. WinForms chỉ hiển thị và gọi phương thức.

## B. Các file bạn sẽ tạo hoặc sửa

- Tạo trong `src/OopGame.Core/Game/`: `DaySummary.cs`, `Restaurant.cs`
- Tạo trong `src/OopGame.WinForms/`: `BuyForm.cs`, `BuyForm.Designer.cs`
  (trong Visual Studio: chuột phải project `OopGame.WinForms`, chọn **Add > Form (Windows Forms)...**, đặt tên `BuyForm.cs`)
- Sửa: `src/OopGame.WinForms/MainForm.cs`, `src/OopGame.WinForms/MainForm.Designer.cs`
- Di chuyển file test `Part4GameTests.cs` (Bước 3)

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

Bấm **Current branch > New branch**, gõ tên: `phan-4-quan-va-giao-dien`, bấm **Create branch**, rồi bấm **Publish branch**.

Kiểm tra ô **Current branch** đang ghi `phan-4-quan-va-giao-dien`. Từ giờ mọi thứ bạn làm nằm trên nhánh này, không ảnh hưởng tới ai.

### Bước 3. Bật bài kiểm tra của phần bạn

1. Trong thư mục code, vào `tests\OopGame.Tests\Pending\`.
2. **Cắt** (Ctrl+X) file `Part4GameTests.cs`.
3. **Dán** (Ctrl+V) ra thư mục cha `tests\OopGame.Tests\`.

Lúc này build sẽ báo lỗi đỏ vì các lớp chưa có. Đó là bình thường, làm xong phần của bạn là hết lỗi.
**Chỉ di chuyển file của phần mình**, không đụng các file khác trong `Pending`.

### Bước 4. Nhờ AI viết code

1. Gửi cho AI (ChatGPT, Gemini, Claude...) **toàn bộ mục D. PROMPT CHO AI** ở cuối file này.
   Cách dễ nhất: gửi luôn cả file `.md` này cho AI, kèm câu: *"Làm đúng theo mục D. PROMPT CHO AI trong file này."*
2. AI trả về nhiều file code, mỗi file có ghi đường dẫn.
3. Tạo từng file trong Visual Studio:
   - Ở **Solution Explorer** (khung bên phải), chuột phải vào thư mục `Game` trong project `OopGame.Core`.
     Chưa có thư mục thì chuột phải vào project `OopGame.Core`, chọn **Add > New Folder**, đặt tên `Game`.
   - Chọn **Add > Class...**, gõ **đúng tên file** AI đưa, bấm **Add**.
   - Xoá hết nội dung Visual Studio tự sinh, dán code của AI vào, bấm Ctrl+S.
4. **Đọc hiểu từng file.** Chỗ nào chưa hiểu thì hỏi lại AI: *"Giải thích đoạn này cho người mới học"*. Thầy có thể hỏi vấn đáp.

### Bước 5. Build và chạy kiểm tra

1. Chọn **Build > Build Solution** (Ctrl+Shift+B). Khung **Error List** phải không còn lỗi (Errors = 0).
   Còn lỗi: copy nguyên văn dòng lỗi gửi AI, kèm câu *"Build bị lỗi này, sửa giúp"*.
2. Chọn **Test > Test Explorer**, bấm **Run All Tests** (nút hai tam giác xanh).
   Mọi bài trong `Part4GameTests` phải có **dấu tích xanh**.
   Bài nào đỏ: bấm vào để xem thông báo, copy gửi AI kèm câu *"Bài kiểm tra này bị đỏ, hãy sửa code, không sửa file test"*.
3. **Tuyệt đối không sửa file test** để cho xanh. Nhóm trưởng sẽ chạy lại để kiểm tra.
4. Bấm **F5**, game vẫn mở lên bình thường là được.

### Bước 6. Commit (lưu lại), chia nhỏ từng việc

GitHub Desktop hiện danh sách file thay đổi ở khung bên trái. Mỗi lần commit:
tích chọn các file của việc đó (bỏ tích file khác), gõ ô **Summary**, bấm **Commit to phan-4-quan-va-giao-dien**.

Nên chia như sau:

| Lần | Chọn các file | Summary ghi |
| --- | --- | --- |
| 1 | `Part4GameTests.cs` (vừa di chuyển, sẽ hiện 2 dòng: xoá ở Pending, thêm ở ngoài) | `Bật bài kiểm tra Phần 4` |
| 2 | `DaySummary.cs` | `Thêm DaySummary tổng kết ngày` |
| 3 | `Restaurant.cs` | `Thêm Restaurant điều phối quán` |
| 4 | `BuyForm.cs`, `BuyForm.Designer.cs` | `Thêm form Nhập hàng` |
| 5 | `MainForm.cs`, `MainForm.Designer.cs` | `Nối giao diện chính với Restaurant` |

Mỗi commit ghi rõ đã làm gì. Thầy xem lịch sử commit để biết bạn làm phần nào.

### Bước 7. Đẩy code lên GitHub

Bấm **Push origin** ở thanh trên cùng của GitHub Desktop.

### Bước 8. Tạo Pull request (nộp bài cho nhóm trưởng)

1. Trong GitHub Desktop bấm **Create Pull Request** (hoặc **Branch > Create pull request**). Trang GitHub mở ra.
2. Dòng trên cùng phải là: `base: main` ← `compare: phan-4-quan-va-giao-dien`.
3. Ô **Title** điền: `Phần 4: Quán, ngày làm việc và giao diện`
4. Ô mô tả, copy mẫu này rồi điền vào chỗ trống:

   ```
   Closes #4

   ## Đã làm
   - (liệt kê các lớp đã tạo)

   ## Kiểm tra
   - Build: 0 lỗi
   - Test Explorer: Part4GameTests xanh hết (__/__ bài)
   - F5: game vẫn mở bình thường
   ```

5. Bấm **Create pull request**, rồi nhắn nhóm trưởng vào duyệt.

### Bước 9. Sửa theo góp ý (nếu có)

Nhóm trưởng góp ý ngay trong Pull request. Bạn sửa code **trên cùng nhánh** `phan-4-quan-va-giao-dien`, commit, bấm **Push origin**.
Pull request tự cập nhật, không cần tạo cái mới.
Khi nhóm trưởng bấm **Merge** là xong. Issue #4 tự đóng, tên bạn nằm trong lịch sử của repo.

### Lỗi thường gặp

| Hiện tượng | Cách xử lý |
| --- | --- |
| Mở `OopGame.sln` báo không hỗ trợ .NET 9 hoặc lỗi `NETSDK1045` | Cập nhật Visual Studio 2022 qua Visual Studio Installer |
| Clone không thấy repo `oop-game` | Chưa bấm Accept lời mời (Bước 0) |
| Push bị từ chối, báo không có quyền | Chưa Accept lời mời, hoặc GitHub Desktop đang đăng nhập tài khoản khác |
| Lỡ sửa code khi đang ở nhánh `main` | Cứ tạo nhánh mới (Bước 2). GitHub Desktop hỏi thì chọn **Bring my changes to phan-4-quan-va-giao-dien** |
| Test Explorer trống trơn | Build lại (Ctrl+Shift+B) rồi đợi vài giây |
| Báo **conflict** | Không xoá code của người khác, nhắn nhóm trưởng cùng xử lý |

## D. PROMPT CHO AI

> Copy **từ dòng "BẮT ĐẦU PROMPT" đến hết file** gửi cho AI, hoặc gửi cả file này cho AI.

==================== BẮT ĐẦU PROMPT ====================

Bạn là một anh/chị khoá trên giỏi C#, vui tính, đang kèm một bạn sinh viên năm nhất làm đồ án môn Lập trình hướng đối tượng. Hãy viết code cho **Phần 4: Quán, ngày làm việc và giao diện**, **đúng chính xác** theo đặc tả bên dưới.

**Giọng văn khi giải thích:** thân thiện, vui vẻ, dễ hiểu, xưng "mình" và gọi "bạn". Dùng ví dụ đời thường trong quán ăn (món, khách, bếp) để giải thích khái niệm OOP. Có thể đùa nhẹ theo phong cách của game. **Nhưng code và kiến thức kỹ thuật phải chính xác tuyệt đối:** đùa ở lời giải thích, không đùa trong code.

### D1. Bối cảnh dự án

- Game **"Quán Ăn Bận Rộn"**, viết bằng C# .NET 9 và Windows Forms. Người chơi điều hành một quán ăn sinh viên với thực đơn "bựa" (Phở Gõ Deadline, Bánh Mì Không Người Yêu, Trà Sữa Full Topping Cháy Ví...). Khách vào gọi món, người chơi nấu rồi phục vụ để nhận tiền và tip. Khách chờ lâu thì bỏ về, quán mất uy tín. Hết nguyên liệu thì nhập thêm.
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
- Phần này ghép 3 phần kia lại: Phần 1, 2, 3 đã làm xong và đã có trong repo. Giao diện công khai (public API) của chúng được tóm tắt ở mục D4.1. Chỉ dùng đúng các thành viên đó.

### D2. Yêu cầu bắt buộc

1. **Đúng chính xác** namespace, tên lớp, tên và kiểu của thuộc tính, phương thức, tham số, giá trị trả về như đặc tả. Không đổi tên, không thêm hay bớt tham số. Được thêm thành viên `private` nếu cần.
2. Chỉ tạo file trong `src/OopGame.Core/Game/` và `src/OopGame.WinForms/`. **Không sửa** bất kỳ file nào đã có (nhất là `Dish.cs`, `Ingredients.cs`, `IUpdatable.cs` và các file test). Không thêm thư viện NuGet.
3. Mỗi lớp một file, tên file trùng tên lớp. Dùng file-scoped namespace (`namespace OopGame.Core.Xxx;`).
4. Code phải **dễ hiểu với sinh viên năm nhất** để trả lời vấn đáp. Áp dụng cho code bạn viết, không áp dụng cho file test có sẵn:
   - Dùng `for`, `foreach`, `if` rõ ràng.
   - **Không dùng LINQ** (`Where`, `Select`, `Any`, `All`, `FirstOrDefault`, `ToList`...), không dùng lambda, không dùng `var`, không dùng `record`.
   - Phương thức và constructor viết thân bằng `{ }` đầy đủ. Constructor rỗng chỉ gọi lớp cha được viết gọn một dòng: `: base(...) { }`.
   - Được dùng cú pháp khởi tạo collection, ví dụ `new Dictionary<string, int> { { Ingredients.Beef, 1 } }`.
   - Chỉ được dùng `=>` cho **thuộc tính chỉ đọc một dòng**, ví dụ `public bool IsLeaving => Patience == 0;`, `public int Count => _dishes.Count;`, `public override string Slogan => "Một ly trà, ba tiếng chém gió.";`.
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

    /// <summary>
    /// Câu slogan vui của món, hiện trên giao diện khi chọn món.
    /// Mỗi món con tự viết câu của mình.
    /// </summary>
    public abstract string Slogan { get; }

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
    public const string InstantNoodle = "Mì gói";
    public const string Milk = "Sữa";
    public const string Pearl = "Trân châu";

    public static IReadOnlyList<string> All { get; } = new[]
    {
        RiceNoodle, Vermicelli, Beef, Pork, Rice, Bread, Egg, Vegetables, Tea, Ice, InstantNoodle, Milk, Pearl
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

### D4. Đặc tả Phần 4

**Ngoại lệ so với D2.2:** riêng Phần 4 **được sửa** `src/OopGame.WinForms/MainForm.cs` và `src/OopGame.WinForms/MainForm.Designer.cs`. Mọi file có sẵn khác vẫn không được sửa.

#### D4.1. Public API của Phần 1, 2, 3 (đã có trong repo, chỉ dùng)

```csharp
// namespace OopGame.Core.Menu
public abstract class MainDish : Dish { }      // ToString(): "[Món chính] Phở Gõ Deadline - 45.000đ"
public abstract class Drink : Dish { }         // ToString(): "[Đồ uống] Trà Đá Chém Gió - 5.000đ"
// 7 món: Pho, BunBo, ComTam, BanhMi, MiTom (MainDish) và TraDa, TraSua (Drink), constructor không tham số.
// Dish còn có: public abstract string Slogan { get; }   ví dụ "Ăn xong chạy deadline xuyên đêm."
public class MenuBook
{
    public MenuBook();                           // có sẵn 7 món
    public int Count { get; }
    public IReadOnlyList<Dish> GetAll();
    public Dish GetRandom(Random random);
    public Dish? FindByName(string name);
}

// namespace OopGame.Core.Customers
public abstract class Customer : IUpdatable
{
    public string Name { get; }
    public Dish Order { get; }
    public int MaxPatience { get; }
    public int Patience { get; }
    public int WaitedSeconds { get; }
    public bool IsLeaving { get; }               // true khi Patience == 0
    public abstract string TypeName { get; }     // "Khách vãng lai" / "Đại gia" / "Reviewer khó tính"
    public void Update(int elapsedSeconds);
    public abstract int CalculateTip();
    public abstract string GetThankYouMessage();  // câu nói khi được phục vụ
    public abstract string GetLeavingMessage();   // câu nói khi bỏ về
    public override string ToString();           // "Anh Shipper (Đại gia) gọi Phở Gõ Deadline - kiên nhẫn 20/25"
}
public class CustomerFactory
{
    public CustomerFactory(Random random);
    public Customer CreateRandom(IReadOnlyList<Dish> menu);
}

// namespace OopGame.Core.Cooking
public class Inventory
{
    public int GetAmount(string ingredient);
    public void Add(string ingredient, int amount);    // amount <= 0 thì ném ArgumentOutOfRangeException
    public bool HasIngredients(Dish dish);
    public bool TryConsume(Dish dish);
    public IReadOnlyDictionary<string, int> GetAll();
}
public class Supplier
{
    public int GetPrice(string ingredient);            // không bán thì ném ArgumentException
    public int GetTotalPrice(string ingredient, int amount);
    public IReadOnlyList<string> GetAvailableIngredients();
}
public class CookingOrder
{
    public Dish Dish { get; }
    public int RemainingSeconds { get; }
    public bool IsDone { get; }
    public override string ToString();                 // "Phở Gõ Deadline - còn 3 giây"
}
public class Kitchen : IUpdatable
{
    public const int MaxSlots = 2;
    public Kitchen(Inventory inventory);
    public IReadOnlyList<CookingOrder> CookingOrders { get; }
    public IReadOnlyList<Dish> ReadyDishes { get; }    // món nấu xong được thêm vào CUỐI danh sách
    public bool IsFull { get; }
    public bool StartCooking(Dish dish);               // bếp đầy hoặc thiếu nguyên liệu thì false
    public void Update(int elapsedSeconds);
    public bool TakeReadyDish(Dish dish);              // tìm theo Name
}
```

#### D4.2. Lớp `DaySummary` (file `src/OopGame.Core/Game/DaySummary.cs`, namespace `OopGame.Core.Game`)

```csharp
public class DaySummary
{
    public DaySummary(int day)
    public int Day { get; }
    public int CustomersServed { get; private set; }
    public int CustomersLeft { get; private set; }
    public int Revenue { get; private set; }       // tổng tiền thu (giá món + tip)
    public int Expenses { get; private set; }      // tổng tiền nhập hàng
    public int Profit => Revenue - Expenses;

    public void RecordServed(int amount)   // amount < 0 thì ném ArgumentOutOfRangeException; CustomersServed++, Revenue += amount
    public void RecordLeft()               // CustomersLeft++
    public void RecordExpense(int amount)  // amount < 0 thì ném ArgumentOutOfRangeException; Expenses += amount

    public override string ToString()
    // Đúng 6 dòng, nối bằng "\n" (KHÔNG dùng Environment.NewLine), tiền định dạng kiểu Việt Nam
    // bằng amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + "đ":
    // Tổng kết ngày 2
    // Đã phục vụ: 2 khách
    // Khách bỏ về: 1 khách
    // Doanh thu: 200.000đ
    // Chi phí: 30.000đ
    // Lợi nhuận: 170.000đ
}
```

#### D4.3. Lớp `Restaurant` (file `src/OopGame.Core/Game/Restaurant.cs`, namespace `OopGame.Core.Game`)

```csharp
public class Restaurant
{
    public const int OpenHour = 8;                       // mở cửa 08:00
    public const int CloseHour = 20;                     // đóng cửa 20:00
    public const int MinutesPerTick = 10;                // mỗi Tick() = 10 phút trong game
    public const int StartingMoney = 200000;
    public const int MaxReputation = 100;
    public const int MaxWaitingCustomers = 5;
    public const int StartingStockPerIngredient = 5;
    public const int ReputationLostPerLeave = 10;
    public const int ReputationGainedPerServe = 2;
    public const int CustomerChancePercent = 30;         // mỗi Tick có 30% khả năng có khách mới

    private readonly Random _random;
    private readonly CustomerFactory _customerFactory;
    private readonly List<Customer> _waitingCustomers = new List<Customer>();

    public Restaurant(Random random)
    // Lưu random. _customerFactory = new CustomerFactory(random) (dùng CHUNG đối tượng random).
    // Menu = new MenuBook(); Inventory = new Inventory(); Kitchen = new Kitchen(Inventory); Supplier = new Supplier();
    // Thêm vào kho StartingStockPerIngredient (5) phần cho MỖI nguyên liệu trong Ingredients.All.
    // Money = StartingMoney; Reputation = MaxReputation; Day = 0; MinutesOfDay = OpenHour * 60; IsOpen = false.

    public MenuBook Menu { get; }
    public Inventory Inventory { get; }
    public Kitchen Kitchen { get; }
    public Supplier Supplier { get; }
    public int Money { get; private set; }
    public int Reputation { get; private set; }
    public int Day { get; private set; }                 // 0 khi chưa mở ngày nào
    public int MinutesOfDay { get; private set; }        // số phút tính từ 0 giờ, ví dụ 08:00 là 480
    public string ClockText { get; }                     // "HH:mm", ví dụ "08:00", "19:50", "20:00"
    public bool IsOpen { get; private set; }
    public bool IsGameOver => Reputation <= 0;
    public IReadOnlyList<Customer> WaitingCustomers { get; }   // _waitingCustomers.AsReadOnly()
    public DaySummary? CurrentSummary { get; private set; }    // null khi chưa mở ngày nào

    public bool OpenDay()
    // Đang mở hoặc IsGameOver thì trả về false.
    // Ngược lại: Day++; MinutesOfDay = OpenHour * 60; IsOpen = true; CurrentSummary = new DaySummary(Day); trả về true.

    public List<string> Tick()
    // Trả về danh sách câu thông báo (log) của giây này. Làm ĐÚNG thứ tự:
    // 1. Nếu !IsOpen thì trả về List rỗng, không làm gì khác.
    // 2. MinutesOfDay += MinutesPerTick.
    // 3. int readyBefore = Kitchen.ReadyDishes.Count; Kitchen.Update(1);
    //    với mỗi món ở vị trí readyBefore trở về sau trong Kitchen.ReadyDishes, thêm log "Ting! {tên món} ra lò."
    // 4. Duyệt một BẢN SAO của danh sách khách (new List<Customer>(_waitingCustomers)), với mỗi khách:
    //    gọi Update(1); nếu IsLeaving thì xoá khỏi _waitingCustomers,
    //    Reputation = Math.Max(0, Reputation - ReputationLostPerLeave),
    //    if (CurrentSummary != null) CurrentSummary.RecordLeft();   (quán đang mở thì CurrentSummary luôn khác null)
    //    thêm log "{Name} bỏ về: \"{GetLeavingMessage()}\" (-10 uy tín)".
    // 5. Nếu _waitingCustomers.Count < MaxWaitingCustomers VÀ _random.Next(100) < CustomerChancePercent:
    //    tạo khách bằng _customerFactory.CreateRandom(Menu.GetAll()), thêm vào _waitingCustomers,
    //    thêm log "{Name} ({TypeName}) bước vào, gọi {Order.Name}".
    // 6. Nếu IsGameOver: IsOpen = false; xoá hết khách đang chờ; thêm log "Uy tín về 0. Quán dính phốt, phải đóng cửa!".
    //    Ngược lại nếu MinutesOfDay >= CloseHour * 60: IsOpen = false; xoá hết khách đang chờ
    //    (không trừ uy tín); thêm log "20:00 rồi, đóng cửa đi ngủ thôi!".
    // 7. Trả về danh sách log.

    public bool StartCooking(Dish dish)
    // Chỉ nấu khi quán đang mở: trả về IsOpen && Kitchen.StartCooking(dish).

    public bool Serve(Customer customer)
    // Trả về false nếu: quán chưa mở, HOẶC khách không có trong _waitingCustomers,
    // HOẶC Kitchen.TakeReadyDish(customer.Order) trả về false (món chưa nấu xong).
    // Ngược lại: int earned = customer.Order.Price + customer.CalculateTip();
    // Money += earned; Reputation = Math.Min(MaxReputation, Reputation + ReputationGainedPerServe);
    // if (CurrentSummary != null) CurrentSummary.RecordServed(earned); xoá khách khỏi _waitingCustomers; trả về true.

    public bool BuyIngredient(string ingredient, int amount)
    // Trả về false nếu amount <= 0, HOẶC nguyên liệu không có trong Supplier.GetAvailableIngredients(),
    // HOẶC tổng giá (Supplier.GetTotalPrice) lớn hơn Money.
    // Ngược lại: Money -= tổng giá; Inventory.Add(ingredient, amount);
    // nếu CurrentSummary khác null thì CurrentSummary.RecordExpense(tổng giá); trả về true.
    // Được mua cả lúc quán đang đóng cửa.
}
```

#### D4.4. Giao diện WinForms (project `src/OopGame.WinForms`)

`MainForm.Designer.cs` đã có sẵn các control. **Giữ nguyên tên các control**:

| Control | Kiểu | Dùng để |
| --- | --- | --- |
| `lblMoney` | Label | `"Tiền: 200.000đ"` |
| `lblReputation` | Label | `"Uy tín: 100"` |
| `lblDay` | Label | `"Ngày 1"` |
| `lblClock` | Label | `"08:00"` |
| `lstCustomers` | ListBox | khách đang chờ (mỗi dòng là một đối tượng `Customer`, hiện bằng `ToString()`) |
| `lstMenu` | ListBox | thực đơn (mỗi dòng là một `Dish`) |
| `lblReadyTitle` | Label | đổi chữ thành `"Bếp"` |
| `lstReady` | ListBox | món đang nấu: `"(đang nấu) " + order.ToString()`; món xong: `"(xong) " + dish.Name` |
| `lstLog` | ListBox | nhật ký, dòng mới nhất ở cuối và tự cuộn xuống |
| `btnOpen`, `btnCook`, `btnServe`, `btnBuy` | Button | 4 nút |

Nội dung hiện tại của `src/OopGame.WinForms/MainForm.cs`:

```csharp
namespace OopGame.WinForms;

/// <summary>
/// Màn hình chính của quán. Hiện tại các nút chỉ ghi log,
/// phần nối với logic quán là task 4 trong TASKS.md.
/// </summary>
public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        AddLog("Chào mừng đến Quán Ăn Bận Rộn! Các nút chưa có logic, xem TASKS.md.");
    }

    private void AddLog(string message)
    {
        lstLog.Items.Add(message);
        lstLog.TopIndex = lstLog.Items.Count - 1;
    }

    private void btnOpen_Click(object? sender, EventArgs e)
    {
        AddLog("Bạn bấm: Mở cửa");
    }

    private void btnCook_Click(object? sender, EventArgs e)
    {
        AddLog("Bạn bấm: Nấu món");
    }

    private void btnServe_Click(object? sender, EventArgs e)
    {
        AddLog("Bạn bấm: Phục vụ");
    }

    private void btnBuy_Click(object? sender, EventArgs e)
    {
        AddLog("Bạn bấm: Nhập hàng");
    }
}
```

Nội dung hiện tại của `src/OopGame.WinForms/MainForm.Designer.cs` (khi sửa, trả về **toàn bộ** file sau khi sửa):

```csharp
namespace OopGame.WinForms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        lblMoney = new Label();
        lblReputation = new Label();
        lblDay = new Label();
        lblClock = new Label();
        lblCustomersTitle = new Label();
        lstCustomers = new ListBox();
        lblMenuTitle = new Label();
        lstMenu = new ListBox();
        lblReadyTitle = new Label();
        lstReady = new ListBox();
        lstLog = new ListBox();
        btnOpen = new Button();
        btnCook = new Button();
        btnServe = new Button();
        btnBuy = new Button();
        SuspendLayout();
        //
        // lblMoney
        //
        lblMoney.AutoSize = true;
        lblMoney.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblMoney.Location = new Point(20, 15);
        lblMoney.Name = "lblMoney";
        lblMoney.Size = new Size(90, 25);
        lblMoney.TabIndex = 0;
        lblMoney.Text = "Tiền: 0đ";
        //
        // lblReputation
        //
        lblReputation.AutoSize = true;
        lblReputation.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblReputation.Location = new Point(240, 15);
        lblReputation.Name = "lblReputation";
        lblReputation.Size = new Size(120, 25);
        lblReputation.TabIndex = 1;
        lblReputation.Text = "Uy tín: 100";
        //
        // lblDay
        //
        lblDay.AutoSize = true;
        lblDay.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblDay.Location = new Point(460, 15);
        lblDay.Name = "lblDay";
        lblDay.Size = new Size(70, 25);
        lblDay.TabIndex = 2;
        lblDay.Text = "Ngày 1";
        //
        // lblClock
        //
        lblClock.AutoSize = true;
        lblClock.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblClock.Location = new Point(660, 15);
        lblClock.Name = "lblClock";
        lblClock.Size = new Size(60, 25);
        lblClock.TabIndex = 3;
        lblClock.Text = "08:00";
        //
        // lblCustomersTitle
        //
        lblCustomersTitle.AutoSize = true;
        lblCustomersTitle.Location = new Point(20, 52);
        lblCustomersTitle.Name = "lblCustomersTitle";
        lblCustomersTitle.Size = new Size(110, 20);
        lblCustomersTitle.TabIndex = 4;
        lblCustomersTitle.Text = "Khách đang chờ";
        //
        // lstCustomers
        //
        lstCustomers.FormattingEnabled = true;
        lstCustomers.Location = new Point(20, 75);
        lstCustomers.Name = "lstCustomers";
        lstCustomers.Size = new Size(270, 224);
        lstCustomers.TabIndex = 5;
        //
        // lblMenuTitle
        //
        lblMenuTitle.AutoSize = true;
        lblMenuTitle.Location = new Point(305, 52);
        lblMenuTitle.Name = "lblMenuTitle";
        lblMenuTitle.Size = new Size(70, 20);
        lblMenuTitle.TabIndex = 6;
        lblMenuTitle.Text = "Thực đơn";
        //
        // lstMenu
        //
        lstMenu.FormattingEnabled = true;
        lstMenu.Location = new Point(305, 75);
        lstMenu.Name = "lstMenu";
        lstMenu.Size = new Size(270, 224);
        lstMenu.TabIndex = 7;
        //
        // lblReadyTitle
        //
        lblReadyTitle.AutoSize = true;
        lblReadyTitle.Location = new Point(590, 52);
        lblReadyTitle.Name = "lblReadyTitle";
        lblReadyTitle.Size = new Size(120, 20);
        lblReadyTitle.TabIndex = 8;
        lblReadyTitle.Text = "Món đã nấu xong";
        //
        // lstReady
        //
        lstReady.FormattingEnabled = true;
        lstReady.Location = new Point(590, 75);
        lstReady.Name = "lstReady";
        lstReady.Size = new Size(270, 224);
        lstReady.TabIndex = 9;
        //
        // lstLog
        //
        lstLog.FormattingEnabled = true;
        lstLog.Location = new Point(20, 315);
        lstLog.Name = "lstLog";
        lstLog.Size = new Size(840, 144);
        lstLog.TabIndex = 10;
        //
        // btnOpen
        //
        btnOpen.Location = new Point(20, 475);
        btnOpen.Name = "btnOpen";
        btnOpen.Size = new Size(195, 50);
        btnOpen.TabIndex = 11;
        btnOpen.Text = "Mở cửa";
        btnOpen.UseVisualStyleBackColor = true;
        btnOpen.Click += btnOpen_Click;
        //
        // btnCook
        //
        btnCook.Location = new Point(235, 475);
        btnCook.Name = "btnCook";
        btnCook.Size = new Size(195, 50);
        btnCook.TabIndex = 12;
        btnCook.Text = "Nấu món";
        btnCook.UseVisualStyleBackColor = true;
        btnCook.Click += btnCook_Click;
        //
        // btnServe
        //
        btnServe.Location = new Point(450, 475);
        btnServe.Name = "btnServe";
        btnServe.Size = new Size(195, 50);
        btnServe.TabIndex = 13;
        btnServe.Text = "Phục vụ";
        btnServe.UseVisualStyleBackColor = true;
        btnServe.Click += btnServe_Click;
        //
        // btnBuy
        //
        btnBuy.Location = new Point(665, 475);
        btnBuy.Name = "btnBuy";
        btnBuy.Size = new Size(195, 50);
        btnBuy.TabIndex = 14;
        btnBuy.Text = "Nhập hàng";
        btnBuy.UseVisualStyleBackColor = true;
        btnBuy.Click += btnBuy_Click;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(880, 545);
        Controls.Add(lblMoney);
        Controls.Add(lblReputation);
        Controls.Add(lblDay);
        Controls.Add(lblClock);
        Controls.Add(lblCustomersTitle);
        Controls.Add(lstCustomers);
        Controls.Add(lblMenuTitle);
        Controls.Add(lstMenu);
        Controls.Add(lblReadyTitle);
        Controls.Add(lstReady);
        Controls.Add(lstLog);
        Controls.Add(btnOpen);
        Controls.Add(btnCook);
        Controls.Add(btnServe);
        Controls.Add(btnBuy);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quán Ăn Bận Rộn";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblMoney;
    private Label lblReputation;
    private Label lblDay;
    private Label lblClock;
    private Label lblCustomersTitle;
    private ListBox lstCustomers;
    private Label lblMenuTitle;
    private ListBox lstMenu;
    private Label lblReadyTitle;
    private ListBox lstReady;
    private ListBox lstLog;
    private Button btnOpen;
    private Button btnCook;
    private Button btnServe;
    private Button btnBuy;
}
```

Quy định chung cho `MainForm` và `BuyForm`:

- Đầu file thêm `using OopGame.Core.Game;`, `using OopGame.Core.Menu;`, `using OopGame.Core.Customers;`, `using OopGame.Core.Cooking;` (dùng cái nào thì thêm cái đó).
- Mọi số tiền hiện trên giao diện định dạng kiểu Việt Nam: `amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + "đ"` (cần `using System.Globalization;`). Nên viết một hàm `private static string FormatMoney(int amount)` dùng chung trong mỗi form.
- Gắn sự kiện kiểu bình thường của WinForms trong file Designer, ví dụ `btnOpen.Click += btnOpen_Click;`. Không dùng lambda.
- Lấy đối tượng đang chọn trong ListBox bằng `as`, ví dụ `Dish? dish = lstMenu.SelectedItem as Dish;` rồi kiểm tra `null`.
- Giao diện **chỉ gọi phương thức public của `Restaurant`**, không tự tính tiền, tự tính tip hay tự sửa kho.

Yêu cầu cho `MainForm`:

1. Trường:
   - `private readonly Restaurant _restaurant = new Restaurant(new Random());`
   - `private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();`
2. Constructor (sau `InitializeComponent()`):
   - `_timer.Interval = 1000; _timer.Tick += OnTimerTick;`
   - Đổ từng món trong `_restaurant.Menu.GetAll()` vào `lstMenu.Items`.
   - Gọi `RefreshView()`, rồi ghi log `"Chào mừng đến Quán Ăn Bận Rộn! Bấm Mở cửa để bắt đầu."`.
   - Trong Designer gắn `lstMenu.SelectedIndexChanged += lstMenu_SelectedIndexChanged;`. Khi chọn một món thì log `"{Name}: {Slogan}"`.
3. `RefreshView()`:
   - `lblMoney.Text = "Tiền: " + FormatMoney(_restaurant.Money)`; `lblReputation.Text = "Uy tín: " + Reputation`; `lblDay.Text = "Ngày " + Day`; `lblClock.Text = ClockText`.
   - Vẽ lại `lstCustomers` từ `WaitingCustomers`. Giữ nguyên khách đang được chọn nếu khách đó vẫn còn trong danh sách.
   - Vẽ lại `lstReady`: trước là các món trong `Kitchen.CookingOrders` (`"(đang nấu) " + order.ToString()`), sau là các món trong `Kitchen.ReadyDishes` (`"(xong) " + dish.Name`).
   - **Gọi `RefreshView()` sau mỗi lần bấm bất kỳ nút nào** và sau mỗi `OnTimerTick`.
4. `btnOpen_Click`:
   - Gọi `OpenDay()`. Thành công thì `_timer.Start()`, ghi log `"Ngày {Day} bắt đầu, quán mở cửa!"`.
   - Thất bại: nếu `IsGameOver` thì log `"Bạn đã thua, không mở cửa được nữa."`, ngược lại log `"Quán đang mở cửa rồi."`.
5. `btnCook_Click`:
   - Lấy món đang chọn trong `lstMenu`. Chưa chọn thì log `"Hãy chọn một món trong thực đơn."`.
   - `StartCooking` thành công thì log `"Bắt đầu nấu {tên món}"`.
   - Thất bại thì log `"Không nấu được: quán chưa mở, bếp đang đầy hoặc thiếu nguyên liệu."`.
6. `btnServe_Click`:
   - Lấy khách đang chọn trong `lstCustomers`. Chưa chọn thì log `"Hãy chọn một khách đang chờ."`.
   - Lưu `int moneyBefore = _restaurant.Money;` rồi gọi `Serve`.
   - Thành công thì log `"{Name}: \"{GetThankYouMessage()}\" (+" + FormatMoney(_restaurant.Money - moneyBefore) + ")"`. **Không** tự gọi `CalculateTip()` trong giao diện.
   - Thất bại thì log `"Chưa phục vụ được {Name}: món {Order.Name} chưa nấu xong."`.
7. `btnBuy_Click`:
   - `using (BuyForm form = new BuyForm(_restaurant)) { form.ShowDialog(this); }` rồi `RefreshView()`.
8. `OnTimerTick(object? sender, EventArgs e)`:
   - Gọi `_restaurant.Tick()`, thêm từng dòng log vào `lstLog`, rồi `RefreshView()`.
   - Nếu sau đó `!_restaurant.IsOpen`: `_timer.Stop()`, nếu `CurrentSummary != null` thì hiện `MessageBox.Show(CurrentSummary.ToString(), "Tổng kết ngày")`.
   - Nếu `IsGameOver`: hiện thêm `MessageBox.Show("Uy tín về 0. Bạn đã thua!", "Thua cuộc")` và đặt `btnOpen.Enabled = false`.
9. Dừng timer khi đóng form: trong Designer thêm `FormClosed += MainForm_FormClosed;`, trong `MainForm_FormClosed` gọi `_timer.Stop()`.
10. Trong `MainForm.Designer.cs` đổi `lblReadyTitle.Text` thành `"Bếp"`. Không đổi tên control nào khác.

Yêu cầu cho `BuyForm` (thêm bằng **Add > Form (Windows Forms)...**, có `BuyForm.cs` và `BuyForm.Designer.cs`):

1. Constructor `public BuyForm(Restaurant restaurant)`: lưu vào trường `private readonly Restaurant _restaurant;`, đổ `Supplier.GetAvailableIngredients()` vào `cboIngredient`, chọn mục đầu tiên, gọi `RefreshLabels()`.
2. Form: `Text = "Nhập hàng"`, `FormBorderStyle = FixedDialog`, `StartPosition = CenterParent`, `MaximizeBox = false`, `MinimizeBox = false`, kích thước khoảng 420 x 260.
3. Các control (đặt đúng tên):

   | Tên | Kiểu | Ghi chú |
   | --- | --- | --- |
   | `cboIngredient` | ComboBox | `DropDownStyle = DropDownList` |
   | `numAmount` | NumericUpDown | `Minimum = 1`, `Maximum = 50`, `Value = 1` |
   | `lblPrice` | Label | `"Tổng giá: " + FormatMoney(...)` |
   | `lblStock` | Label | `"Trong kho: {số lượng}"` của nguyên liệu đang chọn |
   | `lblMoney` | Label | `"Tiền hiện có: " + FormatMoney(Money)` (label riêng của BuyForm) |
   | `btnBuyNow` | Button | chữ `"Mua"` |
   | `btnClose` | Button | chữ `"Đóng"`, bấm thì `Close()` |

4. `RefreshLabels()`: cập nhật 3 label theo nguyên liệu và số lượng đang chọn. Giá lấy từ `Supplier.GetTotalPrice`, số trong kho lấy từ `Inventory.GetAmount`.
5. `cboIngredient.SelectedIndexChanged` và `numAmount.ValueChanged` cùng gắn vào một hàm `OnSelectionChanged`, hàm này gọi `RefreshLabels()`.
6. `btnBuyNow_Click`: gọi `_restaurant.BuyIngredient(nguyên liệu, (int)numAmount.Value)`. Thành công thì `RefreshLabels()`. Thất bại thì `MessageBox.Show("Không đủ tiền hoặc số lượng không hợp lệ.", "Nhập hàng")`.

### D5. Bài kiểm tra phải qua

File `tests/OopGame.Tests/Part4GameTests.cs` (đã có sẵn, không được sửa):

```csharp
using OopGame.Core;
using OopGame.Core.Customers;
using OopGame.Core.Game;

namespace OopGame.Tests;

// Bài kiểm tra cho Phần 4: Quán và ngày làm việc. Cần Phần 1, 2, 3 đã xong. Không sửa file này.
public class Part4GameTests
{
    private static void TickMany(Restaurant restaurant, int count)
    {
        for (int i = 0; i < count; i++)
        {
            restaurant.Tick();
        }
    }

    [Fact]
    public void NewRestaurant_HasStartingValues()
    {
        Restaurant restaurant = new Restaurant(new Random(1));
        Assert.Equal(200000, restaurant.Money);
        Assert.Equal(100, restaurant.Reputation);
        Assert.Equal(0, restaurant.Day);
        Assert.False(restaurant.IsOpen);
        Assert.False(restaurant.IsGameOver);
        Assert.Equal("08:00", restaurant.ClockText);
        Assert.Equal(7, restaurant.Menu.Count);
        Assert.Equal(5, restaurant.Inventory.GetAmount(Ingredients.Beef));
        Assert.Empty(restaurant.WaitingCustomers);
        Assert.Null(restaurant.CurrentSummary);
    }

    [Fact]
    public void OpenDay_StartsDayOne_AndCannotOpenTwice()
    {
        Restaurant restaurant = new Restaurant(new Random(1));
        Assert.True(restaurant.OpenDay());
        Assert.Equal(1, restaurant.Day);
        Assert.True(restaurant.IsOpen);
        Assert.Equal("08:00", restaurant.ClockText);
        Assert.Equal(1, restaurant.CurrentSummary!.Day);
        Assert.False(restaurant.OpenDay());
    }

    [Fact]
    public void Tick_WhenClosed_DoesNothing()
    {
        Restaurant restaurant = new Restaurant(new Random(1));
        Assert.Empty(restaurant.Tick());
        Assert.Equal("08:00", restaurant.ClockText);
    }

    [Fact]
    public void Tick_AdvancesTenMinutes_AndClosesAt20h()
    {
        Restaurant restaurant = new Restaurant(new Random(1));
        restaurant.OpenDay();
        restaurant.Tick();
        Assert.Equal("08:10", restaurant.ClockText);
        TickMany(restaurant, 70);
        Assert.Equal("19:50", restaurant.ClockText);
        Assert.True(restaurant.IsOpen);
        restaurant.Tick();
        Assert.Equal("20:00", restaurant.ClockText);
        Assert.False(restaurant.IsOpen);
        Assert.Empty(restaurant.WaitingCustomers);
    }

    [Fact]
    public void Customers_ArriveButNeverMoreThanFive()
    {
        Restaurant restaurant = new Restaurant(new Random(7));
        restaurant.OpenDay();
        int maxSeen = 0;
        for (int i = 0; i < 60; i++)
        {
            restaurant.Tick();
            maxSeen = Math.Max(maxSeen, restaurant.WaitingCustomers.Count);
        }
        Assert.InRange(maxSeen, 1, 5);
    }

    [Fact]
    public void CustomersLeaving_LowerReputation()
    {
        Restaurant restaurant = new Restaurant(new Random(3));
        restaurant.OpenDay();
        TickMany(restaurant, 72);
        Assert.True(restaurant.CurrentSummary!.CustomersLeft > 0);
        Assert.Equal(100 - 10 * restaurant.CurrentSummary.CustomersLeft, restaurant.Reputation);
    }

    [Fact]
    public void BuyIngredient_SpendsMoneyAndAddsStock()
    {
        Restaurant restaurant = new Restaurant(new Random(1));
        restaurant.OpenDay();
        Assert.True(restaurant.BuyIngredient(Ingredients.Beef, 2));
        Assert.Equal(170000, restaurant.Money);
        Assert.Equal(7, restaurant.Inventory.GetAmount(Ingredients.Beef));
        Assert.Equal(30000, restaurant.CurrentSummary!.Expenses);
    }

    [Fact]
    public void BuyIngredient_FailsWhenInvalidOrTooExpensive()
    {
        Restaurant restaurant = new Restaurant(new Random(1));
        Assert.False(restaurant.BuyIngredient(Ingredients.Beef, 0));
        Assert.False(restaurant.BuyIngredient("Tôm hùm", 1));
        Assert.False(restaurant.BuyIngredient(Ingredients.Beef, 100));
        Assert.Equal(200000, restaurant.Money);
    }

    [Fact]
    public void Serve_PaysPriceAndTip_AndRemovesCustomer()
    {
        Restaurant restaurant = new Restaurant(new Random(5));
        restaurant.OpenDay();
        while (restaurant.WaitingCustomers.Count == 0)
        {
            restaurant.Tick();
        }
        Customer customer = restaurant.WaitingCustomers[0];
        Assert.True(restaurant.StartCooking(customer.Order));
        while (restaurant.Kitchen.ReadyDishes.Count == 0)
        {
            restaurant.Tick();
        }
        int tip = customer.CalculateTip();

        Assert.True(restaurant.Serve(customer));
        Assert.Equal(200000 + customer.Order.Price + tip, restaurant.Money);
        Assert.DoesNotContain(customer, restaurant.WaitingCustomers);
        Assert.Equal(1, restaurant.CurrentSummary!.CustomersServed);
        Assert.False(restaurant.Serve(customer));
    }

    [Fact]
    public void Serve_FailsWhenDishNotReady()
    {
        Restaurant restaurant = new Restaurant(new Random(5));
        restaurant.OpenDay();
        while (restaurant.WaitingCustomers.Count == 0)
        {
            restaurant.Tick();
        }
        Assert.False(restaurant.Serve(restaurant.WaitingCustomers[0]));
        Assert.Equal(200000, restaurant.Money);
    }

    [Fact]
    public void DaySummary_ComputesProfit_AndFormatsText()
    {
        DaySummary summary = new DaySummary(2);
        summary.RecordServed(150000);
        summary.RecordServed(50000);
        summary.RecordLeft();
        summary.RecordExpense(30000);
        Assert.Equal(2, summary.CustomersServed);
        Assert.Equal(1, summary.CustomersLeft);
        Assert.Equal(170000, summary.Profit);
        Assert.Equal(
            "Tổng kết ngày 2\nĐã phục vụ: 2 khách\nKhách bỏ về: 1 khách\nDoanh thu: 200.000đ\nChi phí: 30.000đ\nLợi nhuận: 170.000đ",
            summary.ToString());
    }
}
```

### D6. Cách trả lời

Trả lời theo đúng thứ tự sau:

1. Danh sách các file sẽ tạo, ghi đường dẫn đầy đủ.
2. Nội dung **đầy đủ** của từng file, mỗi file một khối code riêng, ghi đường dẫn ngay phía trên khối. Không viết tắt bằng "...", không bỏ sót phần nào.
3. Giải thích từng lớp bằng giọng vui, dễ hiểu: lớp đó làm gì trong quán, thể hiện tính chất OOP nào (đóng gói, kế thừa, đa hình, trừu tượng), ở đoạn code nào (trích đoạn code ra). Dùng ví dụ món ăn, khách, bếp cho dễ nhớ.
4. Tự rà lại: đối chiếu từng bài kiểm tra ở mục D5 với code, nói rõ vì sao code qua bài đó.
5. Năm câu hỏi vấn đáp thầy có thể hỏi về phần này, kèm câu trả lời ngắn để bạn sinh viên tự trả lời được.
6. Cuối cùng, nhắc bạn ấy một câu: dù có AI viết hộ thì vẫn phải hiểu rõ phần chung (lớp `Dish`, `Ingredients`, `IUpdatable` và cách các phần ghép với nhau), vì thầy hỏi là phải trả lời được.

==================== HẾT PROMPT ====================
