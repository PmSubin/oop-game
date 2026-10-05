# Hướng dẫn GitHub cho người mới

Không cần gõ lệnh. Dùng **GitHub Desktop** (app có nút bấm) và **Visual Studio**.

## Một lần duy nhất lúc đầu

1. Tạo tài khoản ở https://github.com nếu chưa có, rồi gửi **username** cho nhóm trưởng.
2. Mở email, bấm **Accept invitation** trong thư mời vào repo `oop-game`.
3. Cài **GitHub Desktop**: https://desktop.github.com, đăng nhập bằng tài khoản GitHub.
4. Trong GitHub Desktop: **File > Clone repository**, chọn `oop-game`, bấm **Clone**.
   Code sẽ được tải về máy.

## Mỗi lần làm task

1. **Lấy code mới nhất**: trong GitHub Desktop, chọn nhánh `main`, bấm **Fetch origin** rồi **Pull origin**.
2. **Tạo nhánh riêng**: bấm **Current branch > New branch**, đặt tên theo task, ví dụ `phan-1-nhan-vat`.
   Mỗi người làm trên nhánh riêng nên không đè code của nhau.
3. **Viết code**: mở `OopGame.sln` bằng Visual Studio, code, bấm `F5` chạy thử cho chắc không lỗi.
4. **Lưu lại (commit)**: quay lại GitHub Desktop, góc dưới bên trái ghi một câu ngắn mô tả đã làm gì,
   ví dụ `Thêm lớp Warrior`, bấm **Commit to phan-1-nhan-vat**.
   Nên commit nhiều lần nhỏ, mỗi lần xong một việc. Thầy nhìn số commit để biết mình làm đều.
5. **Đẩy lên GitHub (push)**: bấm **Push origin** (lần đầu là **Publish branch**).
6. **Gửi bài (Pull request)**: bấm **Create Pull Request**, trang GitHub mở ra.
   Ở phần mô tả ghi `Closes #1` (thay số 1 bằng số issue của mình), bấm **Create pull request**.
7. Nhóm trưởng xem rồi bấm **Merge**. Code của bạn vào nhánh `main`, issue tự đóng.

## Lưu ý

- **Không ai commit thẳng vào `main`**, luôn làm trên nhánh riêng.
- Commit phải bằng **tài khoản của chính mình**, đừng nhờ người khác commit hộ, nếu không thầy sẽ không thấy phần đóng góp của bạn.
- Bị báo **conflict** (2 người sửa cùng một chỗ): đừng xoá code của người khác, nhắn nhóm cùng xử lý.
