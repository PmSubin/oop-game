# Phần 1: Thực đơn và món ăn

| Issue | Nhánh | Độ khó | Thời gian ước tính |
| --- | --- | --- | --- |
| [#1](https://github.com/PmSubin/oop-game/issues/1) | `phan-1-thuc-don` | Dễ | 3 đến 4 giờ |

## A. Bạn làm gì (đọc trước)

Chúc mừng, bạn là **bếp trưởng kiêm người viết thực đơn** của Quán Ăn Bận Rộn. Quán này không bán món bình thường:

| Món | Giá | Slogan |
| --- | --- | --- |
| Phở Gõ Deadline | 45.000đ | Ăn xong chạy deadline xuyên đêm. |
| Bún Bò Cay Như Người Yêu Cũ | 45.000đ | Cay xé lưỡi, nhớ mãi không quên. |
| Cơm Tấm Cứu Đói Cuối Tháng | 40.000đ | Ví mỏng nhưng bụng vẫn phải no. |
| Bánh Mì Không Người Yêu | 25.000đ | Có thịt, có rau, chỉ thiếu người yêu. |
| Mì Tôm Trứng Mùa Thi | 20.000đ | Món ăn quốc dân của sinh viên ôn thi. |
| Trà Đá Chém Gió | 5.000đ | Một ly trà, ba tiếng chém gió. |
| Trà Sữa Full Topping Cháy Ví | 55.000đ | Uống một ly, nhịn ăn ba bữa. |

Việc của bạn:

- **2 lớp cha trung gian:** `MainDish` (món chính) và `Drink` (đồ uống), cả hai kế thừa lớp `Dish` có sẵn.
- **7 món ở bảng trên:** mỗi món là một lớp riêng, có giá, thời gian nấu, nguyên liệu và câu slogan.
- **`MenuBook`:** cuốn thực đơn chứa 7 món. Dùng để lấy cả danh sách, bốc ngẫu nhiên một món (cho khách gọi), hoặc tìm món theo tên.

**OOP bạn khoe được với thầy:**

- **Kế thừa nhiều cấp:** `Dish` → `MainDish` → `Pho`. Phở là món chính, món chính là món ăn.
- **Trừu tượng:** `Dish`, `MainDish`, `Drink` là `abstract`. Không ai gọi được món tên "Món ăn" chung chung, phải là món cụ thể.
- **Đa hình:** cùng gọi `GetIngredients()` hay `Slogan` nhưng mỗi món trả về một kiểu. `MainDish` và `Drink` tự in tên kiểu khác nhau (override `ToString()`).
- **Đóng gói:** `MenuBook` giữ danh sách món ở dạng `private`. Công thức món trả ra ngoài là bản chỉ đọc, không ai lén sửa công thức Phở được.

## B. Các file bạn sẽ tạo

Tất cả nằm trong `src/OopGame.Core/Menu/` (thư mục đã có file `Dish.cs`, **không sửa file đó**):

`MainDish.cs`, `Drink.cs`, `Pho.cs`, `BunBo.cs`, `ComTam.cs`, `BanhMi.cs`, `MiTom.cs`, `TraDa.cs`, `TraSua.cs`, `MenuBook.cs`

Và di chuyển file test `Part1MenuTests.cs` (Bước 3).

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

Bấm **Current branch > New branch**, gõ tên: `phan-1-thuc-don`, bấm **Create branch**, rồi bấm **Publish branch**.

Kiểm tra ô **Current branch** đang ghi `phan-1-thuc-don`. Từ giờ mọi thứ bạn làm nằm trên nhánh này, không ảnh hưởng tới ai.

### Bước 3. Bật bài kiểm tra của phần bạn

1. Trong thư mục code, vào `tests\OopGame.Tests\Pending\`.
2. **Cắt** (Ctrl+X) file `Part1MenuTests.cs`.
3. **Dán** (Ctrl+V) ra thư mục cha `tests\OopGame.Tests\`.

Lúc này build sẽ báo lỗi đỏ vì các lớp chưa có. Đó là bình thường, làm xong phần của bạn là hết lỗi.
**Chỉ di chuyển file của phần mình**, không đụng các file khác trong `Pending`.

### Bước 4. Nhờ AI viết code

1. Gửi cho AI (ChatGPT, Gemini, Claude...) **toàn bộ mục D. PROMPT CHO AI** ở cuối file này.
   Cách dễ nhất: gửi luôn cả file `.md` này cho AI, kèm câu: *"Làm đúng theo mục D. PROMPT CHO AI trong file này."*
2. AI trả về nhiều file code, mỗi file có ghi đường dẫn.
3. Tạo từng file trong Visual Studio:
   - Ở **Solution Explorer** (khung bên phải), chuột phải vào thư mục `Menu` trong project `OopGame.Core`.
     Chưa có thư mục thì chuột phải vào project `OopGame.Core`, chọn **Add > New Folder**, đặt tên `Menu`.
   - Chọn **Add > Class...**, gõ **đúng tên file** AI đưa, bấm **Add**.
   - Xoá hết nội dung Visual Studio tự sinh, dán code của AI vào, bấm Ctrl+S.
4. **Đọc hiểu từng file.** Chỗ nào chưa hiểu thì hỏi lại AI: *"Giải thích đoạn này cho người mới học"*. Thầy có thể hỏi vấn đáp.

### Bước 5. Build và chạy kiểm tra

1. Chọn **Build > Build Solution** (Ctrl+Shift+B). Khung **Error List** phải không còn lỗi (Errors = 0).
   Còn lỗi: copy nguyên văn dòng lỗi gửi AI, kèm câu *"Build bị lỗi này, sửa giúp"*.
2. Chọn **Test > Test Explorer**, bấm **Run All Tests** (nút hai tam giác xanh).
   Mọi bài trong `Part1MenuTests` phải có **dấu tích xanh**.
   Bài nào đỏ: bấm vào để xem thông báo, copy gửi AI kèm câu *"Bài kiểm tra này bị đỏ, hãy sửa code, không sửa file test"*.
3. **Tuyệt đối không sửa file test** để cho xanh. Nhóm trưởng sẽ chạy lại để kiểm tra.
4. Bấm **F5**, game vẫn mở lên bình thường là được.

### Bước 6. Commit (lưu lại), chia nhỏ từng việc

GitHub Desktop hiện danh sách file thay đổi ở khung bên trái. Mỗi lần commit:
tích chọn các file của việc đó (bỏ tích file khác), gõ ô **Summary**, bấm **Commit to phan-1-thuc-don**.

Nên chia như sau:

| Lần | Chọn các file | Summary ghi |
| --- | --- | --- |
| 1 | `Part1MenuTests.cs` (vừa di chuyển, sẽ hiện 2 dòng: xoá ở Pending, thêm ở ngoài) | `Bật bài kiểm tra Phần 1` |
| 2 | `MainDish.cs`, `Drink.cs` | `Thêm lớp cha MainDish và Drink` |
| 3 | `Pho.cs`, `BunBo.cs`, `ComTam.cs`, `BanhMi.cs`, `MiTom.cs` | `Thêm 5 món chính` |
| 4 | `TraDa.cs`, `TraSua.cs` | `Thêm 2 món đồ uống` |
| 5 | `MenuBook.cs` | `Thêm lớp MenuBook` |

Mỗi commit ghi rõ đã làm gì. Thầy xem lịch sử commit để biết bạn làm phần nào.

### Bước 7. Đẩy code lên GitHub

Bấm **Push origin** ở thanh trên cùng của GitHub Desktop.

### Bước 8. Tạo Pull request (nộp bài cho nhóm trưởng)

1. Trong GitHub Desktop bấm **Create Pull Request** (hoặc **Branch > Create pull request**). Trang GitHub mở ra.
2. Dòng trên cùng phải là: `base: main` ← `compare: phan-1-thuc-don`.
3. Ô **Title** điền: `Phần 1: Thực đơn và món ăn`
4. Ô mô tả, copy mẫu này rồi điền vào chỗ trống:

   ```
   Closes #1

   ## Đã làm
   - (liệt kê các lớp đã tạo)

   ## Kiểm tra
   - Build: 0 lỗi
   - Test Explorer: Part1MenuTests xanh hết (__/__ bài)
   - F5: game vẫn mở bình thường
   ```

5. Bấm **Create pull request**, rồi nhắn nhóm trưởng vào duyệt.

### Bước 9. Sửa theo góp ý (nếu có)

Nhóm trưởng góp ý ngay trong Pull request. Bạn sửa code **trên cùng nhánh** `phan-1-thuc-don`, commit, bấm **Push origin**.
Pull request tự cập nhật, không cần tạo cái mới.
Khi nhóm trưởng bấm **Merge** là xong. Issue #1 tự đóng, tên bạn nằm trong lịch sử của repo.

### Lỗi thường gặp

| Hiện tượng | Cách xử lý |
| --- | --- |
| Mở `OopGame.sln` báo không hỗ trợ .NET 9 hoặc lỗi `NETSDK1045` | Cập nhật Visual Studio 2022 qua Visual Studio Installer |
| Clone không thấy repo `oop-game` | Chưa bấm Accept lời mời (Bước 0) |
| Push bị từ chối, báo không có quyền | Chưa Accept lời mời, hoặc GitHub Desktop đang đăng nhập tài khoản khác |
| Lỡ sửa code khi đang ở nhánh `main` | Cứ tạo nhánh mới (Bước 2). GitHub Desktop hỏi thì chọn **Bring my changes to phan-1-thuc-don** |
| Test Explorer trống trơn | Build lại (Ctrl+Shift+B) rồi đợi vài giây |
| Báo **conflict** | Không xoá code của người khác, nhắn nhóm trưởng cùng xử lý |

## D. PROMPT CHO AI

> Copy **từ dòng "BẮT ĐẦU PROMPT" đến hết file** gửi cho AI, hoặc gửi cả file này cho AI.

==================== BẮT ĐẦU PROMPT ====================

Bạn là một anh/chị khoá trên giỏi C#, vui tính, đang kèm một bạn sinh viên năm nhất làm đồ án môn Lập trình hướng đối tượng. Hãy viết code cho **Phần 1: Thực đơn và món ăn**, **đúng chính xác** theo đặc tả bên dưới.

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
- Phần này chỉ dựa vào các file dùng chung ở mục D3, không phụ thuộc phần nào khác.

### D2. Yêu cầu bắt buộc

1. **Đúng chính xác** namespace, tên lớp, tên và kiểu của thuộc tính, phương thức, tham số, giá trị trả về như đặc tả. Không đổi tên, không thêm hay bớt tham số. Được thêm thành viên `private` nếu cần.
2. Chỉ tạo file trong `src/OopGame.Core/Menu/`. **Không sửa** bất kỳ file nào đã có (nhất là `Dish.cs`, `Ingredients.cs`, `IUpdatable.cs` và các file test). Không thêm thư viện NuGet.
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

### D4. Đặc tả Phần 1

**Thư mục:** `src/OopGame.Core/Menu/`. **Namespace:** `OopGame.Core.Menu`.

#### Lớp `MainDish` (file `MainDish.cs`)

```csharp
public abstract class MainDish : Dish
{
    protected MainDish(string name, int price, int cookTimeSeconds) : base(name, price, cookTimeSeconds) { }
    public override string ToString()   // trả về "[Món chính] " + base.ToString()
}
```

Ví dụ: `new Pho().ToString()` trả về `"[Món chính] Phở Gõ Deadline - 45.000đ"`.

#### Lớp `Drink` (file `Drink.cs`)

```csharp
public abstract class Drink : Dish
{
    protected Drink(string name, int price, int cookTimeSeconds) : base(name, price, cookTimeSeconds) { }
    public override string ToString()   // trả về "[Đồ uống] " + base.ToString()
}
```

Ví dụ: `new TraSua().ToString()` trả về `"[Đồ uống] Trà Sữa Full Topping Cháy Ví - 55.000đ"`.

#### Bảy món cụ thể

- Mỗi món là một `public class`, mỗi lớp một file.
- Constructor `public`, **không tham số**, gọi `: base(tên, giá, thời gian nấu)` theo bảng dưới.
- Override `public override string Slogan => "...";` trả về đúng câu slogan trong bảng, **giữ nguyên dấu chấm cuối câu**.
- Override `public override IReadOnlyDictionary<string, int> GetIngredients()`:
  - Trả về công thức: khoá là **hằng số trong lớp `Ingredients`** (ví dụ `Ingredients.Beef`, không gõ chuỗi `"Thịt bò"`), giá trị là số lượng.
  - Lưu công thức trong một trường `private static readonly Dictionary<string, int> _recipe`.
  - Trả về `new ReadOnlyDictionary<string, int>(_recipe)`, để bên ngoài không sửa được công thức.

| Lớp | Kế thừa | Name | Price | CookTimeSeconds | Nguyên liệu (mỗi thứ số lượng 1) |
| --- | --- | --- | --- | --- | --- |
| `Pho` | `MainDish` | `"Phở Gõ Deadline"` | 45000 | 6 | `Ingredients.RiceNoodle`, `Ingredients.Beef`, `Ingredients.Vegetables` |
| `BunBo` | `MainDish` | `"Bún Bò Cay Như Người Yêu Cũ"` | 45000 | 6 | `Ingredients.Vermicelli`, `Ingredients.Beef`, `Ingredients.Vegetables` |
| `ComTam` | `MainDish` | `"Cơm Tấm Cứu Đói Cuối Tháng"` | 40000 | 5 | `Ingredients.Rice`, `Ingredients.Pork`, `Ingredients.Egg` |
| `BanhMi` | `MainDish` | `"Bánh Mì Không Người Yêu"` | 25000 | 3 | `Ingredients.Bread`, `Ingredients.Pork`, `Ingredients.Vegetables` |
| `MiTom` | `MainDish` | `"Mì Tôm Trứng Mùa Thi"` | 20000 | 2 | `Ingredients.InstantNoodle`, `Ingredients.Egg` |
| `TraDa` | `Drink` | `"Trà Đá Chém Gió"` | 5000 | 1 | `Ingredients.Tea`, `Ingredients.Ice` |
| `TraSua` | `Drink` | `"Trà Sữa Full Topping Cháy Ví"` | 55000 | 2 | `Ingredients.Tea`, `Ingredients.Milk`, `Ingredients.Pearl`, `Ingredients.Ice` |

| Lớp | `Slogan` |
| --- | --- |
| `Pho` | `"Ăn xong chạy deadline xuyên đêm."` |
| `BunBo` | `"Cay xé lưỡi, nhớ mãi không quên."` |
| `ComTam` | `"Ví mỏng nhưng bụng vẫn phải no."` |
| `BanhMi` | `"Có thịt, có rau, chỉ thiếu người yêu."` |
| `MiTom` | `"Món ăn quốc dân của sinh viên ôn thi."` |
| `TraDa` | `"Một ly trà, ba tiếng chém gió."` |
| `TraSua` | `"Uống một ly, nhịn ăn ba bữa."` |

#### Lớp `MenuBook` (file `MenuBook.cs`)

```csharp
public class MenuBook
{
    private readonly List<Dish> _dishes;          // danh sách món, bên ngoài không truy cập trực tiếp

    public MenuBook()
    // Tạo sẵn đủ 7 món, theo thứ tự: Pho, BunBo, ComTam, BanhMi, MiTom, TraDa, TraSua.

    public int Count => _dishes.Count;
    // Số món trong thực đơn (là 7).

    public IReadOnlyList<Dish> GetAll()
    // Trả về _dishes.AsReadOnly(). KHÔNG trả thẳng _dishes.

    public Dish GetRandom(Random random)
    // Trả về một món ngẫu nhiên: _dishes[random.Next(_dishes.Count)].
    // Dùng đúng đối tượng random được truyền vào, không tự tạo new Random().

    public Dish? FindByName(string name)
    // Duyệt danh sách bằng foreach, so tên bằng
    // string.Equals(dish.Name, name, StringComparison.CurrentCultureIgnoreCase)
    // để không phân biệt hoa thường ("phở gõ deadline", "PHỞ GÕ DEADLINE" đều ra Phở).
    // Không tìm thấy thì trả về null.
}
```

### D5. Bài kiểm tra phải qua

File `tests/OopGame.Tests/Part1MenuTests.cs` (đã có sẵn, không được sửa):

```csharp
using OopGame.Core;
using OopGame.Core.Menu;

namespace OopGame.Tests;

// Bài kiểm tra cho Phần 1: Thực đơn và món ăn. Không sửa file này.
public class Part1MenuTests
{
    [Theory]
    [InlineData(typeof(Pho), "Phở Gõ Deadline", 45000, 6)]
    [InlineData(typeof(BunBo), "Bún Bò Cay Như Người Yêu Cũ", 45000, 6)]
    [InlineData(typeof(ComTam), "Cơm Tấm Cứu Đói Cuối Tháng", 40000, 5)]
    [InlineData(typeof(BanhMi), "Bánh Mì Không Người Yêu", 25000, 3)]
    [InlineData(typeof(MiTom), "Mì Tôm Trứng Mùa Thi", 20000, 2)]
    [InlineData(typeof(TraDa), "Trà Đá Chém Gió", 5000, 1)]
    [InlineData(typeof(TraSua), "Trà Sữa Full Topping Cháy Ví", 55000, 2)]
    public void Dish_HasCorrectNamePriceAndCookTime(Type dishType, string name, int price, int cookTime)
    {
        Dish dish = (Dish)Activator.CreateInstance(dishType)!;
        Assert.Equal(name, dish.Name);
        Assert.Equal(price, dish.Price);
        Assert.Equal(cookTime, dish.CookTimeSeconds);
    }

    [Theory]
    [InlineData(typeof(Pho), "Ăn xong chạy deadline xuyên đêm.")]
    [InlineData(typeof(BunBo), "Cay xé lưỡi, nhớ mãi không quên.")]
    [InlineData(typeof(ComTam), "Ví mỏng nhưng bụng vẫn phải no.")]
    [InlineData(typeof(BanhMi), "Có thịt, có rau, chỉ thiếu người yêu.")]
    [InlineData(typeof(MiTom), "Món ăn quốc dân của sinh viên ôn thi.")]
    [InlineData(typeof(TraDa), "Một ly trà, ba tiếng chém gió.")]
    [InlineData(typeof(TraSua), "Uống một ly, nhịn ăn ba bữa.")]
    public void Dish_HasCorrectSlogan(Type dishType, string slogan)
    {
        Dish dish = (Dish)Activator.CreateInstance(dishType)!;
        Assert.Equal(slogan, dish.Slogan);
    }

    [Fact]
    public void MainDishes_InheritMainDish_AndDrinksInheritDrink()
    {
        Assert.IsAssignableFrom<MainDish>(new Pho());
        Assert.IsAssignableFrom<MainDish>(new BunBo());
        Assert.IsAssignableFrom<MainDish>(new ComTam());
        Assert.IsAssignableFrom<MainDish>(new BanhMi());
        Assert.IsAssignableFrom<MainDish>(new MiTom());
        Assert.IsAssignableFrom<Drink>(new TraDa());
        Assert.IsAssignableFrom<Drink>(new TraSua());
        Assert.True(typeof(MainDish).IsAbstract);
        Assert.True(typeof(Drink).IsAbstract);
    }

    [Fact]
    public void Ingredients_MatchRecipeTable()
    {
        AssertRecipe(new Pho(), Ingredients.RiceNoodle, Ingredients.Beef, Ingredients.Vegetables);
        AssertRecipe(new BunBo(), Ingredients.Vermicelli, Ingredients.Beef, Ingredients.Vegetables);
        AssertRecipe(new ComTam(), Ingredients.Rice, Ingredients.Pork, Ingredients.Egg);
        AssertRecipe(new BanhMi(), Ingredients.Bread, Ingredients.Pork, Ingredients.Vegetables);
        AssertRecipe(new MiTom(), Ingredients.InstantNoodle, Ingredients.Egg);
        AssertRecipe(new TraDa(), Ingredients.Tea, Ingredients.Ice);
        AssertRecipe(new TraSua(), Ingredients.Tea, Ingredients.Milk, Ingredients.Pearl, Ingredients.Ice);
    }

    [Fact]
    public void Recipe_CannotBeChangedFromOutside()
    {
        IReadOnlyDictionary<string, int> recipe = new Pho().GetIngredients();
        Assert.False(recipe is Dictionary<string, int>, "GetIngredients() phải trả về bản chỉ đọc, không trả thẳng Dictionary bên trong.");
    }

    [Fact]
    public void ToString_ShowsCategoryPrefix()
    {
        Assert.Equal("[Món chính] Phở Gõ Deadline - 45.000đ", new Pho().ToString());
        Assert.Equal("[Đồ uống] Trà Sữa Full Topping Cháy Ví - 55.000đ", new TraSua().ToString());
    }

    [Fact]
    public void MenuBook_HasSevenDishes_AndListIsReadOnly()
    {
        MenuBook menu = new MenuBook();
        Assert.Equal(7, menu.Count);
        Assert.Equal(7, menu.GetAll().Count);
        Assert.False(menu.GetAll() is List<Dish>, "GetAll() phải trả về danh sách chỉ đọc, không trả thẳng List bên trong.");
    }

    [Fact]
    public void MenuBook_FindByName_IgnoresCase_AndReturnsNullWhenMissing()
    {
        MenuBook menu = new MenuBook();
        Assert.IsType<Pho>(menu.FindByName("phở gõ deadline"));
        Assert.IsType<TraDa>(menu.FindByName("TRÀ ĐÁ CHÉM GIÓ"));
        Assert.Null(menu.FindByName("Pizza Dứa"));
    }

    [Fact]
    public void MenuBook_GetRandom_ReturnsDishFromMenu()
    {
        MenuBook menu = new MenuBook();
        Random random = new Random(1);
        for (int i = 0; i < 30; i++)
        {
            Assert.Contains(menu.GetRandom(random), menu.GetAll());
        }
    }

    private static void AssertRecipe(Dish dish, params string[] expected)
    {
        IReadOnlyDictionary<string, int> recipe = dish.GetIngredients();
        Assert.Equal(expected.Length, recipe.Count);
        foreach (string ingredient in expected)
        {
            Assert.Equal(1, recipe[ingredient]);
        }
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
