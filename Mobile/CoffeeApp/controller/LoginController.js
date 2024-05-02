import { child, get, getDatabase, ref } from "firebase/database";
import db from "../firebase";
import AsyncStorage from '@react-native-async-storage/async-storage';
import { storeData } from "./StorageController";

/**
 * @notice Handle login
 * @param username username
 * @param password password
 * @returns The result of the operation
 */
const handleLogin = async (username, password) => {
    const dbRef = ref(getDatabase());
    try {
        // Get users from database
        const usersSnapshot = await get(child(dbRef, 'NguoiDung/'));
        const users = usersSnapshot.val();
        let account = null;
        
        if (users) {
            // Login
            for (const [userId, userData] of Object.entries(users)) {
                if (userId.slice(0, 2) === 'KH' && userData.TaiKhoan === username && userData.MatKhau === password) {
                    // Store user data in AsyncStorage
                    await storeData(userData);
                    account = {username, password}
                    return account;
                }
            }
            return account;
        } else {
            return ("Không có dữ liệu người dùng");
        }
    } catch (error) {
        console.error(error);
        return error;
    }
};

export { handleLogin };
