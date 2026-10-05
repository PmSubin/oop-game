# Hướng dẫn làm task

Mỗi phần có một file hướng dẫn riêng, đọc từ trên xuống là làm được. Mỗi file gồm:

- **A, B:** bạn làm gì, tạo những file nào.
- **C:** từng bước từ lúc tải code, tạo nhánh, chạy kiểm tra, cho tới lúc gửi Pull request.
- **D:** đoạn PROMPT để gửi cho AI. Gửi cả file cho AI cũng được.

| Phần | File hướng dẫn | Issue | Độ khó |
| --- | --- | --- | --- |
| 1. Thực đơn và món ăn | [phan-1-thuc-don.md](phan-1-thuc-don.md) | [#1](https://github.com/PmSubin/oop-game/issues/1) | Dễ |
| 2. Khách hàng | [phan-2-khach-hang.md](phan-2-khach-hang.md) | [#2](https://github.com/PmSubin/oop-game/issues/2) | Vừa |
| 3. Bếp và kho nguyên liệu | [phan-3-bep-va-kho.md](phan-3-bep-va-kho.md) | [#3](https://github.com/PmSubin/oop-game/issues/3) | Khó |
| 4. Quán, ngày làm việc và giao diện | [phan-4-quan-va-giao-dien.md](phan-4-quan-va-giao-dien.md) | [#4](https://github.com/PmSubin/oop-game/issues/4) | Khó nhất, làm sau cùng |

## Vì sao có bài kiểm tra

Thư mục `tests/OopGame.Tests/Pending/` chứa sẵn bài kiểm tra cho từng phần.
Bạn làm phần nào thì kéo file của phần đó ra ngoài (Bước 3 trong hướng dẫn).

Chạy **Test > Test Explorer > Run All Tests** trong Visual Studio:

- **Xanh hết:** code đúng đặc tả và ghép được với phần của người khác.
- **Có bài đỏ:** AI làm sai chỗ đó. Gửi thông báo lỗi cho AI để sửa.

Không ai được sửa file test để cho xanh.
