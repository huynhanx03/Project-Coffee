import AsyncStorage from "@react-native-async-storage/async-storage";
import { getDatabase, ref, set, update } from "firebase/database";

const getKh = async () => {
    try {
        const jsonValue = await AsyncStorage.getItem("user");
        return jsonValue != null ? JSON.parse(jsonValue) : null;
    } catch (error) {
        console.log(error);
        return error;
    }
};

const checkOldPassword = async (oldPassword) => {
    const userData = await getKh();
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
    const userData = await getKh();
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
