import { child, get, getDatabase, ref, set } from "firebase/database";

/**
 * @notice Generate a new id for the new user
 * @returns a new id for the new user
 */
const getNewId = async () => {
    const dbRef = ref(getDatabase());
    try {
        const usersSnapshot = await get(child(dbRef, 'KhachHang/'));
        const users = usersSnapshot.val();
        
        if (users) {
            const currentId = parseInt(Object.keys(users)[Object.keys(users).length - 1].slice(2));
            
            const newId = 'KH' + String(currentId + 1).padStart(4, '0');
            return newId;
        } else {
            return 'KH0001';
        }
    } catch (err) {
        console.log(err);
    }
}

/**
 * @notice Verify email is already in use
 */
const verifyEmail = async (email) => {
    // check if email is already in use
    const dbRef = ref(getDatabase());
    try {
        const usersSnapshot = await get(child(dbRef, 'NguoiDung/'));
        const users = usersSnapshot.val();

        if (users) {
            for (const [userId, userData] of Object.entries(users)) {
                if (userData.Email === email) {
                    return false;
                }
            }
        }

        return true;
    } catch (err) {
        console.log(err);
        return err;
    }
}

/**
 * @notice Validate email
 * @param email Email that need validate
 * @returns The result of the operation
 */
const validateEmail = (email) => {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
};

/**
 * @notice Register new user and store information in the database
 * @param username username
 * @param email email
 * @param password password
 * @returns The result of the operation
 */
const Register = async (username, email, password) => {
    if (!validateEmail(email)) {
        return [false, "Email không hợp lệ!"];
    }

    if (!verifyEmail(email)) {
        return [false, "Email đã tồn tại!"];
    }
    const newId = await getNewId();
    const db = getDatabase();
    const currentTime = new Date();
    const dateCreated = currentTime.toLocaleDateString('vi-VN');
    set(ref(db, 'NguoiDung/' + newId), {
        TaiKhoan: username,
        Email: email,
        MatKhau: password,
        VaiTro: "2",
        CCCD_CMND: "",
        DiaChi: "",
        GioiTinh: "",
        HoTen: "",
        MaNguoiDung: newId,
        NgayTao: dateCreated,
        SoDienThoai: "",
        HinhAnh: "",
        NgaySinh: "",
    })

    set(ref(db, 'KhachHang/' + newId), {
        DiemTichLuy: 0,
        MaKhachHang: newId,
    });

    set(ref(db, 'ChiTietMucDoThanThiet/' + newId), {
        MaKhachHang: newId,
        ChiTiet: {
            TT0001: {
                MaMucDoThanThiet: 'TT0001',
                NgayDatDuoc: dateCreated
            }
        }
    })

    return [true, 'Đăng ký thành công!'];
}

export { Register };