# Space Shooting Game

A 2D space shooter built with Unity, featuring wave-based enemy spawning, a leveling system, boss encounters, and a parallax space environment.

---

## Tech Stack

| Công nghệ | Mô tả |
|---|---|
| **Unity** | Game engine chính (2D) |
| **C#** | Ngôn ngữ lập trình |
| **TextMesh Pro** | Hiển thị text UI |
| **Unity Physics 2D** | Va chạm, Rigidbody |
| **Unity Animator** | Animation state machine cho player và boss |
| **Unity Particle System** | Hiệu ứng boost, vụ nổ |

---

## Gameplay

Người chơi điều khiển tàu vũ trụ bay qua không gian, tiêu diệt kẻ địch và né tránh chướng ngại vật. Mục tiêu là sống sót và đạt được điểm kinh nghiệm để nâng cấp vũ khí.

### Điều khiển

| Phím | Hành động |
|---|---|
| `W A S D` / Arrow Keys | Di chuyển |
| `Mouse Left Click` | Bắn |
| `Space` / `Left Alt` | Kích hoạt Boost |
| `Escape` | Pause |

### Cơ chế chính

**Boost**
- Tiêu thụ Energy để tăng tốc độ thế giới, giúp di chuyển nhanh hơn
- Energy tự hồi sinh theo thời gian khi không boost
- Nếu hết Energy, Boost tự tắt

**Hệ thống Level Up**
- Tiêu diệt kẻ địch và thiên thạch để nhận Experience
- Đủ EXP → lên level, tăng máu tối đa và nâng cấp vũ khí
- Vũ khí mạnh hơn bắn nhiều đạn hơn với khoảng cách rộng hơn

**Phaser Weapon**
- Level 1: 1 viên đạn
- Level cao hơn: nhiều viên đạn bắn đồng thời, trải rộng theo chiều dọc

---

## Danh sách kẻ địch

### Critter
Sinh vật nhỏ di chuyển ngẫu nhiên trong màn hình. Không bắn được bằng đạn — chỉ tiêu diệt được bằng cách đâm thẳng vào. Khi tiêu diệt đủ 10 Critter, Boss sẽ xuất hiện.

### Beetlemorph
Di chuyển theo đường **sóng hình sin** (sine wave) theo trục Y, tạo ra quỹ đạo lượn sóng khó đoán. Có nhiều biến thể sprite ngẫu nhiên.

### Locustmorph
Có 2 phase:
- **Idle**: Di chuyển chậm, lơ lửng
- **Charge**: Khi còn dưới 50% máu, lao thẳng về phía trái với tốc độ cao. Có nhiều variant hình dạng khác nhau.

### SquidMorph
Luôn xoay mặt về phía người chơi. Cứ 2 giây phóng ra **SquidCritter** nhắm thẳng vào người chơi.

### SquidCritter
Được phóng ra từ SquidMorph. Sau pha phóng ban đầu, tự động **bám đuổi người chơi** và xoay mặt theo hướng di chuyển.

---

## Boss

### Boss 1 — Charge Boss
Xuất hiện mỗi khi người chơi tiêu diệt đủ **10 Critter**. Có 2 trạng thái:
- **Patrol**: Lơ lửng di chuyển lên xuống ngẫu nhiên
- **Charge**: Lao nhanh về phía trái nhắm vào người chơi

Khi người chơi Boost, Boss điều chỉnh tốc độ di chuyển theo. Có animation riêng cho từng trạng thái.

### Boss 2
Di chuyển theo pattern bounce: lao từ phải sang trái, sau đó quay lại từ trái sang phải với tốc độ cao. Chuyển trạng thái dựa theo vị trí X trên màn hình.

---

## Chướng ngại vật

### Asteroid
- Kích thước ngẫu nhiên (0.5x – 1.5x)
- Chịu được **5 đòn** trước khi bị phá hủy
- Rơi theo hướng ngẫu nhiên, va chạm vật lý thực
- Cho EXP khi bị bắn hạ (không cho EXP nếu bị Boss phá)

### Whale
Sinh vật vũ trụ di chuyển cùng chiều với thế giới. Chạm vào **LostWhale** (cá voi đặc biệt) sẽ hoàn thành màn chơi.

---

## Kiến trúc kỹ thuật

### Singleton Pattern
Các manager dùng chung Singleton để truy cập toàn cục:
- `GameManager` — quản lý tốc độ thế giới, boss spawn, game over
- `PlayerController` — trạng thái người chơi
- `PhaserWeapon` — bắn đạn
- `AudioManager` — phát âm thanh

### Object Pooling
Toàn bộ đạn, kẻ địch, hiệu ứng nổ đều dùng **Object Pool** thay vì `Instantiate/Destroy` để tối ưu performance. Pool tự mở rộng khi cần.

### Hệ thống Wave Spawner
`ObjectSpawner` quản lý spawn theo wave tuần tự. Mỗi wave có:
- Pool đối tượng riêng
- Thời gian spawn interval
- Số lượng object tối đa trước khi chuyển wave tiếp theo

### Enemy Base Class
Tất cả kẻ địch kế thừa từ class `Enemy`, chia sẻ logic chung: nhận damage, flash trắng khi trúng đạn, chết và cho EXP. Từng loại enemy override các phương thức cần thiết.

### Parallax Background
Nhiều lớp background di chuyển với tốc độ khác nhau tạo chiều sâu không gian. Menu cũng có parallax riêng.

---

## Cấu trúc project

```
Assets/
├── Script/
│   ├── Enemies/        # Beetlemorph, Locustmorph, SquidMorph, Boss1, Boss2...
│   ├── Weapons/        # PhaserWeapon, PhaserBullet, Weapon base
│   ├── Obstacles/      # Asteroid, Whale, LostWhale
│   ├── MenuScene/      # MenuManager, MenuParallax
│   ├── Utils/          # FlashWhite, FloatInSpace, FaceMovementDirection...
│   ├── GameManager.cs
│   ├── PlayerController.cs
│   ├── AudioManager.cs
│   ├── UIController.cs
│   ├── ObjectPooler.cs
│   ├── ObjectSpawner.cs
│   └── ParallaxBackground.cs
```

---

## Chạy project

1. Clone repo về máy
2. Mở bằng **Unity Hub** (khuyến nghị Unity 6. trở lên)
3. Mở scene `MainMenu` để bắt đầu từ menu
4. Hoặc mở trực tiếp scene `Level 1` để test gameplay

---

## Tác giả

**Towie1206**
