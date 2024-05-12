import { getDatabase, ref, update } from "firebase/database"
import { getUserData } from "./StorageController";

const updateInfo = async (content, type) => {
    const db = getDatabase();
    const userData = await getUserData();

    try {
        update(ref(db, `NguoiDung/${userData.MaNguoiDung}`), {
            [type]: content
        })

        return [true, "Cập nhật thông tin thành công"]
    } catch (error) {
        console.log(error);
        return [false, "Cập nhật thông tin thất bại"]
    }
}

export { updateInfo }