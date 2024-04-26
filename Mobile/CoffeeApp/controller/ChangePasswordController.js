import AsyncStorage from "@react-native-async-storage/async-storage";
import { getDatabase, ref, set, update } from "firebase/database";
import getUserData from "./StorageController";

const checkOldPassword = async (oldPassword) => {
    const userData = await getUserData();
    if (userData.MatKhau !== oldPassword) {
        return false;
    }

    return true;
};

const checkConfirmPassword = (newPassword, confirmPassword) => {
    if (newPassword !== confirmPassword) {
        return false;
    }

    return true;
};

const changePassword = async (oldPassword, newPassword, confirmPassword) => {
    const userData = await getUserData();
    const db = getDatabase();

    if (await checkOldPassword(oldPassword) === false){
        return [false, "Mật khẩu cũ không đúng"];
    }

    if (checkConfirmPassword(newPassword, confirmPassword) === false)
        return [false, "Mật khẩu xác nhận không đúng"];

    try {
        update(ref(db, `NguoiDung/${userData.MaNguoiDung}/`), {
            MatKhau: newPassword,
        });

        return [true, "Đổi mật khẩu thành công"];
    } catch (error) {
        console.log(error);
        return error;
    }
};

export { changePassword };
