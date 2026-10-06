# Chia task

Hướng dẫn chi tiết từng phần (đặc tả, các bước GitHub, prompt cho AI) nằm trong thư mục [docs/](docs/README.md).

## Bảng phân công

| Phần | Người làm | Hướng dẫn | Issue | Độ khó |
| --- | --- | --- | --- | --- |
| 1. Thực đơn và món ăn | tanbui31251026551-byte | [phan-1-thuc-don.md](docs/phan-1-thuc-don.md) | #1 | Dễ |
| 2. Khách hàng | toanhtan2021-cmyk | [phan-2-khach-hang.md](docs/phan-2-khach-hang.md) | #2 | Vừa |
| 3. Bếp và kho nguyên liệu | DeeKay153 | [phan-3-bep-va-kho.md](docs/phan-3-bep-va-kho.md) | #3 | Khó |
| 4. Quán, ngày làm việc và giao diện | PmSubin | [phan-4-quan-va-giao-dien.md](docs/phan-4-quan-va-giao-dien.md) | #4 | Khó nhất, làm sau cùng |

Phần 1, 2, 3 **làm song song được**, không ai phải chờ ai. Phần 4 ghép 3 phần kia lại.

## Thầy xem ai làm gì ở đâu

1. Tab **Issues**: mỗi phần gán cho ai.
2. Tab **Pull requests**: code của từng người gửi lên, ai gửi, khi nào.
3. **Insights > Contributors**: số commit và số dòng code của từng người.

## File dùng chung, không ai được sửa

| File | Là gì |
| --- | --- |
| `src/OopGame.Core/Menu/Dish.cs` | Lớp cha trừu tượng của mọi món ăn |
| `src/OopGame.Core/IUpdatable.cs` | Interface cho những thứ thay đổi theo từng giây |
| `src/OopGame.Core/Ingredients.cs` | Tên nguyên liệu dùng chung |
| `tests/OopGame.Tests/**` | Bài kiểm tra. Chỉ được di chuyển file test của phần mình ra khỏi `Pending` |

## Làm xong phần chính

Ai còn thời gian thì nhận thêm: hình ảnh món ăn, âm thanh, lưu tiến trình, nâng cấp quán (mua thêm bếp), sự kiện ngẫu nhiên (giờ cao điểm, khách nổi tiếng).
