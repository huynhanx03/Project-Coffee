import AsyncStorage from "@react-native-async-storage/async-storage";
import { getDatabase, ref, set, update } from "firebase/database";
import {getUserData} from "./StorageController";

/**
 * @notice Check if the old password is correct
 * @param oldPassword Get the old password from the user
 * @returns The result of the operation
 */
const checkOldPassword = async (oldPassword) => {
    const userData = await getUserData();
    if (userData.MatKhau !== oldPassword) {
        return false;
    }

    return true;
};

/**
 * @notice Check if the confirm password is similar to the new password
 * @param newPassword Get the new password
 * @param confirmPassword Get the confirm password
 * @returns The result of the operation
 */
const checkConfirmPassword = (newPassword, confirmPassword) => {
    if (newPassword !== confirmPassword) {
        return false;
    }

    return true;
};

/**
 * @notice Change password
 * @param oldPassword old password
 * @param newPassword new password
 * @param confirmPassword confirm password
 * @returns The result of the operation
 */
const changePassword = async (oldPassword, newPassword, confirmPassword, isForgot) => {
    const userData = await getUserData();
    const db = getDatabase();

    if (await checkOldPassword(oldPassword) === false && !isForgot){
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
