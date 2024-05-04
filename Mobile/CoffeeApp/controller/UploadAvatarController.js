import { getDatabase, ref, update } from "firebase/database"
import {getUserData} from "./StorageController"

const uploadImage = async (image) => {
    const userData = await getUserData()
    const db = getDatabase()

    try {
        update(ref(db, `NguoiDung/${userData.MaNguoiDung}/`), {
            HinhAnh: image
        })

        return [true, "Cập nhật thành công"]
    } catch (error) {
        console.log(error)
        return error
    }
}

export {uploadImage}