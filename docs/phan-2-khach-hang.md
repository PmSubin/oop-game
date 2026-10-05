# Phần 2: Khách hàng

| Issue | Nhánh | Độ khó | Thời gian ước tính |
| --- | --- | --- | --- |
| [#2](https://github.com/PmSubin/oop-game/issues/2) | `phan-2-khach-hang` | Vừa | 3 đến 5 giờ |

## A. Bạn làm gì (đọc trước)

Bạn là người **tạo ra khách hàng**. Quán có đủ loại khách, mỗi người một tính:

| Loại | Tính cách | Lúc được phục vụ | Lúc bỏ về |
| --- | --- | --- | --- |
| **Khách vãng lai** (`NormalCustomer`) | Dễ tính, chờ được 40 giây | "Ngon, cảm ơn quán nha!" | "Thôi đi quán khác vậy..." |
| **Đại gia** (`VipCustomer`) | Tip rất đậm nhưng chỉ chờ 25 giây | "Ngon! Khỏi thối tiền thừa." | "Đại gia mà bắt chờ à? Không bao giờ quay lại!" |
| **Reviewer khó tính** (`PickyCustomer`) | Chờ quá 10 giây là bực gấp đôi | Nhanh: "Nhanh đấy, cho 5 sao!", chậm: "Chậm quá... thôi 3 sao." | "Chờ lâu thế này, về viết review 1 sao!" |

Việc của bạn:

- **Lớp cha trừu tượng `Customer`:** mỗi khách có tên, món đã gọi và thanh **kiên nhẫn** giảm dần theo từng giây. Hết kiên nhẫn thì bỏ về.
- **3 loại khách ở bảng trên:** mỗi loại tự tính tiền tip và tự nói câu thoại của mình.
- **`CustomerFactory`:** "máy đẻ khách" ngẫu nhiên, với tên như "Anh Shipper Vội Vàng" hay "Em Sinh Viên Cuối Tháng". Tỉ lệ 60% vãng lai, 20% đại gia, 20% reviewer. Mỗi khách gọi ngẫu nhiên một món.

**OOP bạn thể hiện được (đây là phần thầy hay hỏi nhất về đa hình):**

- **Trừu tượng:** `Customer` là `abstract`, có phương thức `abstract CalculateTip()` mà lớp con bắt buộc tự viết.
- **Kế thừa:** 3 loại khách kế thừa `Customer`.
- **Đa hình:**
  - Mỗi loại khách tính tip khác nhau (override `CalculateTip()`) và nói câu khác nhau (override `GetThankYouMessage()`, `GetLeavingMessage()`).
  - Reviewer khó tính mất kiên nhẫn khác (override `ReducePatience()`).
  - Quán chỉ cần gọi `customer.GetThankYouMessage()`, tự khắc đại gia nói kiểu đại gia, reviewer nói kiểu reviewer. Đây là ví dụ đa hình rất dễ giải thích với thầy.
- **Interface:** `Customer` cài đặt `IUpdatable`, được quán gọi `Update(1)` mỗi giây.
- **Đóng gói:** `Patience` chỉ đọc từ bên ngoài, luôn nằm trong khoảng 0 đến `MaxPatience`.

## B. Các file bạn sẽ tạo

Tất cả nằm trong thư mục mới `src/OopGame.Core/Customers/`:

`Customer.cs`, `NormalCustomer.cs`, `VipCustomer.cs`, `PickyCustomer.cs`, `CustomerFactory.cs`

Và di chuyển file test `Part2CustomerTests.cs` (Bước 3).

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

Bấm **Current branch > New branch**, gõ tên: `phan-2-khach-hang`, bấm **Create branch**, rồi bấm **Publish branch**.

Kiểm tra ô **Current branch** đang ghi `phan-2-khach-hang`. Từ giờ mọi thứ bạn làm nằm trên nhánh này, không ảnh hưởng tới ai.

### Bước 3. Bật bài kiểm tra của phần bạn

1. Trong thư mục code, vào `tests\OopGame.Tests\Pending\`.
2. **Cắt** (Ctrl+X) file `Part2CustomerTests.cs`.
3. **Dán** (Ctrl+V) ra thư mục cha `tests\OopGame.Tests\`.

Lúc này build sẽ báo lỗi đỏ vì các lớp chưa có. Đó là bình thường, làm xong phần của bạn là hết lỗi.
**Chỉ di chuyển file của phần mình**, không đụng các file khác trong `Pending`.

### Bước 4. Nhờ AI viết code

1. Gửi cho AI (ChatGPT, Gemini, Claude...) **toàn bộ mục D. PROMPT CHO AI** ở cuối file này.
   Cách dễ nhất: gửi luôn cả file `.md` này cho AI, kèm câu: *"Làm đúng theo mục D. PROMPT CHO AI trong file này."*
2. AI trả về nhiều file code, mỗi file có ghi đường dẫn.
3. Tạo từng file trong Visual Studio:
   - Ở **Solution Explorer** (khung bên phải), chuột phải vào thư mục `Customers` trong project `OopGame.Core`.
     Chưa có thư mục thì chuột phải vào project `OopGame.Core`, chọn **Add > New Folder**, đặt tên `Customers`.
   - Chọn **Add > Class...**, gõ **đúng tên file** AI đưa, bấm **Add**.
   - Xoá hết nội dung Visual Studio tự sinh, dán code của AI vào, bấm Ctrl+S.
4. **Đọc hiểu từng file.** Chỗ nào chưa hiểu thì hỏi lại AI: *"Giải thích đoạn này cho người mới học"*. Thầy có thể hỏi vấn đáp.

### Bước 5. Build và chạy kiểm tra

1. Chọn **Build > Build Solution** (Ctrl+Shift+B). Khung **Error List** phải không còn lỗi (Errors = 0).
   Còn lỗi: copy nguyên văn dòng lỗi gửi AI, kèm câu *"Build bị lỗi này, sửa giúp"*.
2. Chọn **Test > Test Explorer**, bấm **Run All Tests** (nút hai tam giác xanh).
   Mọi bài trong `Part2CustomerTests` phải có **dấu tích xanh**.
   Bài nào đỏ: bấm vào để xem thông báo, copy gửi AI kèm câu *"Bài kiểm tra này bị đỏ, hãy sửa code, không sửa file test"*.
3. **Tuyệt đối không sửa file test** để cho xanh. Nhóm trưởng sẽ chạy lại để kiểm tra.
4. Bấm **F5**, game vẫn mở lên bình thường là được.

### Bước 6. Commit (lưu lại), chia nhỏ từng việc

GitHub Desktop hiện danh sách file thay đổi ở khung bên trái. Mỗi lần commit:
tích chọn các file của việc đó (bỏ tích file khác), gõ ô **Summary**, bấm **Commit to phan-2-khach-hang**.

Nên chia như sau:

| Lần | Chọn các file | Summary ghi |
| --- | --- | --- |
| 1 | `Part2CustomerTests.cs` (vừa di chuyển, sẽ hiện 2 dòng: xoá ở Pending, thêm ở ngoài) | `Bật bài kiểm tra Phần 2` |
| 2 | `Customer.cs` | `Thêm lớp trừu tượng Customer` |
| 3 | `NormalCustomer.cs`, `VipCustomer.cs`, `PickyCustomer.cs` | `Thêm 3 loại khách` |
| 4 | `CustomerFactory.cs` | `Thêm CustomerFactory tạo khách ngẫu nhiên` |

Mỗi commit ghi rõ đã làm gì. Thầy xem lịch sử commit để biết bạn làm phần nào.

### Bước 7. Đẩy code lên GitHub

Bấm **Push origin** ở thanh trên cùng của GitHub Desktop.

### Bước 8. Tạo Pull request (nộp bài cho nhóm trưởng)

1. Trong GitHub Desktop bấm **Create Pull Request** (hoặc **Branch > Create pull request**). Trang GitHub mở ra.
2. Dòng trên cùng phải là: `base: main` ← `compare: phan-2-khach-hang`.
3. Ô **Title** điền: `Phần 2: Khách hàng`
4. Ô mô tả, copy mẫu này rồi điền vào chỗ trống:

   ```
   Closes #2

   ## Đã làm
   - (liệt kê các lớp đã tạo)

   ## Kiểm tra
   - Build: 0 lỗi
   - Test Explorer: Part2CustomerTests xanh hết (__/__ bài)
   - F5: game vẫn mở bình thường
   ```

5. Bấm **Create pull request**, rồi nhắn nhóm trưởng vào duyệt.

### Bước 9. Sửa theo góp ý (nếu có)

Nhóm trưởng góp ý ngay trong Pull request. Bạn sửa code **trên cùng nhánh** `phan-2-khach-hang`, commit, bấm **Push origin**.
Pull request tự cập nhật, không cần tạo cái mới.
Khi nhóm trưởng bấm **Merge** là xong. Issue #2 tự đóng, tên bạn nằm trong lịch sử của repo.

### Lỗi thường gặp

| Hiện tượng | Cách xử lý |
| --- | --- |
| Mở `OopGame.sln` báo không hỗ trợ .NET 9 hoặc lỗi `NETSDK1045` | Cập nhật Visual Studio 2022 qua Visual Studio Installer |
| Clone không thấy repo `oop-game` | Chưa bấm Accept lời mời (Bước 0) |
| Push bị từ chối, báo không có quyền | Chưa Accept lời mời, hoặc GitHub Desktop đang đăng nhập tài khoản khác |
| Lỡ sửa code khi đang ở nhánh `main` | Cứ tạo nhánh mới (Bước 2). GitHub Desktop hỏi thì chọn **Bring my changes to phan-2-khach-hang** |
| Test Explorer trống trơn | Build lại (Ctrl+Shift+B) rồi đợi vài giây |
| Báo **conflict** | Không xoá code của người khác, nhắn nhóm trưởng cùng xử lý |

## D. PROMPT CHO AI

> Copy **từ dòng "BẮT ĐẦU PROMPT" đến hết file** gửi cho AI, hoặc gửi cả file này cho AI.

==================== BẮT ĐẦU PROMPT ====================

Bạn là một anh/chị khoá trên giỏi C#, vui tính, đang kèm một bạn sinh viên năm nhất làm đồ án môn Lập trình hướng đối tượng. Hãy viết code cho **Phần 2: Khách hàng**, **đúng chính xác** theo đặc tả bên dưới.

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
- Phần này chỉ dựa vào các file dùng chung ở mục D3 (dùng kiểu `Dish` cho món khách gọi). KHÔNG dùng `MenuBook` hay các món cụ thể của Phần 1, vì Phần 1 đang được làm song song.

### D2. Yêu cầu bắt buộc

1. **Đúng chính xác** namespace, tên lớp, tên và kiểu của thuộc tính, phương thức, tham số, giá trị trả về như đặc tả. Không đổi tên, không thêm hay bớt tham số. Được thêm thành viên `private` nếu cần.
2. Chỉ tạo file trong `src/OopGame.Core/Customers/`. **Không sửa** bất kỳ file nào đã có (nhất là `Dish.cs`, `Ingredients.cs`, `IUpdatable.cs` và các file test). Không thêm thư viện NuGet.
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

### D4. Đặc tả Phần 2

**Thư mục:** `src/OopGame.Core/Customers/`. **Namespace:** `OopGame.Core.Customers`. **Cả 5 file** đều cần `using OopGame.Core.Menu;` ở đầu file để dùng `Dish`.

#### Lớp `Customer` (file `Customer.cs`)

```csharp
public abstract class Customer : IUpdatable
{
    public string Name { get; }
    public Dish Order { get; }                  // món khách gọi
    public int MaxPatience { get; }             // số giây kiên nhẫn tối đa
    public int Patience { get; private set; }   // kiên nhẫn còn lại, luôn trong khoảng 0..MaxPatience
    public int WaitedSeconds { get; private set; }   // đã chờ bao nhiêu giây
    public bool IsLeaving => Patience == 0;     // hết kiên nhẫn thì bỏ về
    public abstract string TypeName { get; }    // tên loại khách để hiện lên màn hình: "Khách vãng lai", "Đại gia", "Reviewer khó tính"

    protected Customer(string name, Dish order, int maxPatience)
    // Gán các thuộc tính. Patience bắt đầu bằng maxPatience. WaitedSeconds bắt đầu bằng 0.

    public void Update(int elapsedSeconds)
    // Nếu elapsedSeconds <= 0 thì return, không làm gì.
    // Ngược lại: WaitedSeconds += elapsedSeconds; rồi gọi ReducePatience(elapsedSeconds).
    // LƯU Ý THỨ TỰ: tăng WaitedSeconds TRƯỚC, gọi ReducePatience SAU.

    protected virtual void ReducePatience(int seconds)
    // Mặc định: SetPatience(Patience - seconds).

    protected void SetPatience(int value)
    // Patience = Math.Clamp(value, 0, MaxPatience).
    // Lớp con muốn đổi Patience thì phải gọi hàm này (vì setter của Patience là private).

    public abstract int CalculateTip();
    // Tiền tip khi được phục vụ, dựa vào WaitedSeconds và Order.Price.

    public abstract string GetThankYouMessage();
    // Câu khách nói khi được phục vụ.

    public abstract string GetLeavingMessage();
    // Câu khách nói khi hết kiên nhẫn bỏ về.

    public override string ToString()
    // Định dạng: "{Name} ({TypeName}) gọi {Order.Name} - kiên nhẫn {Patience}/{MaxPatience}"
    // Ví dụ: "Anh Shipper (Đại gia) gọi Phở Gõ Deadline - kiên nhẫn 20/25"
}
```

#### Ba loại khách

- Mỗi lớp có constructor `public Xxx(string name, Dish order)`, tự truyền `MaxPatience` của mình vào `base(name, order, maxPatience)`.
- Override `TypeName`, `CalculateTip()`, `GetThankYouMessage()`, `GetLeavingMessage()`.
- Tiền tip tính bằng phép chia số nguyên: `Order.Price * phần_trăm / 100`.
- Các câu thoại phải **đúng từng chữ, từng dấu câu** như bảng (kể cả dấu `...`, `!`, `?`).

| Lớp | `TypeName` | `MaxPatience` | Kiên nhẫn giảm thế nào | `CalculateTip()` |
| --- | --- | --- | --- | --- |
| `NormalCustomer` | `"Khách vãng lai"` | 40 | Không override, dùng mặc định (mỗi giây giảm 1) | `WaitedSeconds <= 20` thì 10% giá món, ngược lại 0 |
| `VipCustomer` | `"Đại gia"` | 25 | Không override, dùng mặc định | `WaitedSeconds <= 15` thì 30% giá món, ngược lại 10% |
| `PickyCustomer` | `"Reviewer khó tính"` | 40 | **Override `ReducePatience`:** nếu `WaitedSeconds > 10` thì giảm `seconds * 2`, ngược lại giảm `seconds`. Dùng `SetPatience(...)` | `WaitedSeconds <= 10` thì 20% giá món, ngược lại 0 |

| Lớp | `GetThankYouMessage()` | `GetLeavingMessage()` |
| --- | --- | --- |
| `NormalCustomer` | `"Ngon, cảm ơn quán nha!"` | `"Thôi đi quán khác vậy..."` |
| `VipCustomer` | `"Ngon! Khỏi thối tiền thừa."` | `"Đại gia mà bắt chờ à? Không bao giờ quay lại!"` |
| `PickyCustomer` | `WaitedSeconds <= 10` thì `"Nhanh đấy, cho 5 sao!"`, ngược lại `"Chậm quá... thôi 3 sao."` | `"Chờ lâu thế này, về viết review 1 sao!"` |

Ví dụ kiểm tra tay:

- Reviewer khó tính sau 10 lần `Update(1)` có `Patience` = 30.
- Sau thêm 5 lần nữa thì `Patience` = 20 (vì mỗi giây sau giây thứ 10 giảm 2).

#### Lớp `CustomerFactory` (file `CustomerFactory.cs`)

```csharp
public class CustomerFactory
{
    private readonly Random _random;
    private static readonly string[] _names = new string[]
    {
        "Anh Shipper Vội Vàng", "Chị Review Một Sao", "Em Sinh Viên Cuối Tháng", "Anh Code Dạo",
        "Chị Bán Hàng Online", "Bác Bảo Vệ Trường", "Anh Gym Ăn Kiêng", "Cô Hàng Xóm Hóng Chuyện"
    };   // được thêm tên vui khác, tối thiểu 8 tên, không dùng tên người thật

    public CustomerFactory(Random random)
    // Lưu random vào _random. Mọi lựa chọn ngẫu nhiên đều dùng _random, không tự tạo new Random().

    public Customer CreateRandom(IReadOnlyList<Dish> menu)
    // 1. Nếu menu.Count == 0 thì throw new ArgumentException("Thực đơn đang trống.", nameof(menu)).
    // 2. Chọn món: menu[_random.Next(menu.Count)].
    // 3. Chọn tên: _names[_random.Next(_names.Length)].
    // 4. Chọn loại khách: int roll = _random.Next(100);
    //    roll < 60  thì new NormalCustomer(...)
    //    roll < 80  thì new VipCustomer(...)
    //    còn lại    thì new PickyCustomer(...)
}
```

### D5. Bài kiểm tra phải qua

File `tests/OopGame.Tests/Part2CustomerTests.cs` (đã có sẵn, không được sửa):

```csharp
using OopGame.Core;
using OopGame.Core.Customers;
using OopGame.Core.Menu;

namespace OopGame.Tests;

// Bài kiểm tra cho Phần 2: Khách hàng. Không sửa file này.
public class Part2CustomerTests
{
    // Món giả giá 100.000đ để dễ tính tip. Không phụ thuộc Phần 1.
    private sealed class FakeDish : Dish
    {
        public FakeDish() : base("Món thử", 100000, 3) { }

        public override string Slogan => "Slogan thử";

        public override IReadOnlyDictionary<string, int> GetIngredients()
        {
            return new Dictionary<string, int> { [Ingredients.Tea] = 1 };
        }
    }

    private static void Wait(Customer customer, int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            customer.Update(1);
        }
    }

    [Fact]
    public void Customer_IsAbstract_AndImplementsIUpdatable()
    {
        Assert.True(typeof(Customer).IsAbstract);
        Assert.True(typeof(IUpdatable).IsAssignableFrom(typeof(Customer)));
    }

    [Fact]
    public void NewCustomer_StartsWithFullPatience()
    {
        Customer customer = new NormalCustomer("Anh Minh", new FakeDish());
        Assert.Equal("Anh Minh", customer.Name);
        Assert.Equal("Món thử", customer.Order.Name);
        Assert.Equal(40, customer.MaxPatience);
        Assert.Equal(40, customer.Patience);
        Assert.Equal(0, customer.WaitedSeconds);
        Assert.False(customer.IsLeaving);
    }

    [Fact]
    public void TypeNames_AreCorrect()
    {
        Assert.Equal("Khách vãng lai", new NormalCustomer("A", new FakeDish()).TypeName);
        Assert.Equal("Đại gia", new VipCustomer("A", new FakeDish()).TypeName);
        Assert.Equal("Reviewer khó tính", new PickyCustomer("A", new FakeDish()).TypeName);
    }

    [Fact]
    public void Update_WithZeroOrNegative_DoesNothing()
    {
        Customer customer = new NormalCustomer("A", new FakeDish());
        customer.Update(0);
        customer.Update(-5);
        Assert.Equal(40, customer.Patience);
        Assert.Equal(0, customer.WaitedSeconds);
    }

    [Fact]
    public void NormalCustomer_LosesOnePatiencePerSecond()
    {
        Customer customer = new NormalCustomer("A", new FakeDish());
        Wait(customer, 15);
        Assert.Equal(25, customer.Patience);
        Assert.Equal(15, customer.WaitedSeconds);
    }

    [Fact]
    public void VipCustomer_LeavesAfter25Seconds()
    {
        Customer customer = new VipCustomer("A", new FakeDish());
        Wait(customer, 24);
        Assert.False(customer.IsLeaving);
        customer.Update(1);
        Assert.True(customer.IsLeaving);
    }

    [Fact]
    public void PickyCustomer_LosesDoubleAfterTenSeconds()
    {
        Customer customer = new PickyCustomer("A", new FakeDish());
        Wait(customer, 10);
        Assert.Equal(30, customer.Patience);
        Wait(customer, 5);
        Assert.Equal(20, customer.Patience);
    }

    [Fact]
    public void Patience_NeverGoesBelowZero()
    {
        Customer customer = new PickyCustomer("A", new FakeDish());
        Wait(customer, 100);
        Assert.Equal(0, customer.Patience);
        Assert.True(customer.IsLeaving);
    }

    [Fact]
    public void Tips_FollowTheTable()
    {
        Customer normal = new NormalCustomer("A", new FakeDish());
        Wait(normal, 20);
        Assert.Equal(10000, normal.CalculateTip());
        normal.Update(1);
        Assert.Equal(0, normal.CalculateTip());

        Customer vip = new VipCustomer("A", new FakeDish());
        Wait(vip, 15);
        Assert.Equal(30000, vip.CalculateTip());
        vip.Update(1);
        Assert.Equal(10000, vip.CalculateTip());

        Customer picky = new PickyCustomer("A", new FakeDish());
        Wait(picky, 10);
        Assert.Equal(20000, picky.CalculateTip());
        picky.Update(1);
        Assert.Equal(0, picky.CalculateTip());
    }

    [Fact]
    public void ToString_ShowsNameTypeOrderAndPatience()
    {
        Customer customer = new VipCustomer("Anh Shipper", new FakeDish());
        Wait(customer, 5);
        Assert.Equal("Anh Shipper (Đại gia) gọi Món thử - kiên nhẫn 20/25", customer.ToString());
    }

    [Fact]
    public void EachType_SaysItsOwnLines()
    {
        Customer normal = new NormalCustomer("A", new FakeDish());
        Assert.Equal("Ngon, cảm ơn quán nha!", normal.GetThankYouMessage());
        Assert.Equal("Thôi đi quán khác vậy...", normal.GetLeavingMessage());

        Customer vip = new VipCustomer("A", new FakeDish());
        Assert.Equal("Ngon! Khỏi thối tiền thừa.", vip.GetThankYouMessage());
        Assert.Equal("Đại gia mà bắt chờ à? Không bao giờ quay lại!", vip.GetLeavingMessage());

        Customer picky = new PickyCustomer("A", new FakeDish());
        Assert.Equal("Chờ lâu thế này, về viết review 1 sao!", picky.GetLeavingMessage());
    }

    [Fact]
    public void PickyCustomer_ThankYouDependsOnWaitTime()
    {
        Customer picky = new PickyCustomer("A", new FakeDish());
        Wait(picky, 10);
        Assert.Equal("Nhanh đấy, cho 5 sao!", picky.GetThankYouMessage());
        picky.Update(1);
        Assert.Equal("Chậm quá... thôi 3 sao.", picky.GetThankYouMessage());
    }

    [Fact]
    public void Factory_ThrowsOnEmptyMenu()
    {
        CustomerFactory factory = new CustomerFactory(new Random(1));
        Assert.Throws<ArgumentException>(() => factory.CreateRandom(new List<Dish>()));
    }

    [Fact]
    public void Factory_CreatesAllThreeTypes_WithOrderFromMenu()
    {
        CustomerFactory factory = new CustomerFactory(new Random(42));
        List<Dish> menu = new List<Dish> { new FakeDish() };
        int normal = 0;
        int vip = 0;
        int picky = 0;
        for (int i = 0; i < 1000; i++)
        {
            Customer customer = factory.CreateRandom(menu);
            Assert.Same(menu[0], customer.Order);
            Assert.False(string.IsNullOrWhiteSpace(customer.Name));
            if (customer is NormalCustomer)
            {
                normal++;
            }
            else if (customer is VipCustomer)
            {
                vip++;
            }
            else if (customer is PickyCustomer)
            {
                picky++;
            }
        }
        // Tỉ lệ 60/20/20, cho phép lệch một chút vì là ngẫu nhiên.
        Assert.InRange(normal, 520, 680);
        Assert.InRange(vip, 130, 270);
        Assert.InRange(picky, 130, 270);
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
