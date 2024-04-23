import { child, get, getDatabase, ref } from "firebase/database";
import db from "../firebase";
import AsyncStorage from '@react-native-async-storage/async-storage';

const storeData = async (value) => {
    try {
        const jsonValue = JSON.stringify(value)
        await AsyncStorage.setItem('user', jsonValue)
    } catch (error) {
        console.log(error)
    }
}

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
                    storeData(userData);
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
