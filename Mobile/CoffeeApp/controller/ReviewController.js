import { child, get, getDatabase, ref, set } from "firebase/database"
import { getUserData } from "./StorageController"

/**
 * @notice Get new review id in the database
 * @returns new review id in the database
 */
const getNewId = async () => {
    const dbRef = ref(getDatabase())
    const userData = await getUserData()

    try {
        const reviewsSnapshot = await get(child(dbRef, `DanhGia/`))
        const reviews = reviewsSnapshot.val()

        if (reviews) {
            const currentId = parseInt(
                Object.keys(reviews)[Object.keys(reviews).length - 1].slice(2)
            )
            const newId = "DG" + String(currentId + 1).padStart(4, "0")
            return newId
        } else {
            return "DG0001"
        }
    } catch (error) {
        console.log(error)
        return error
    }

}

/**
 * @notice Set review for product
 * @param rating The rating that cus give to the product
 * @param review The review that cus give to the product
 */
const setReview = async (productId, rating, review) => {
    const userData = await getUserData()
    const newId = await getNewId()
    const db = getDatabase()

    const currentDate = new Date();
    const options = { 
        day: '2-digit', 
        month: '2-digit', 
        year: 'numeric', 
        hour: '2-digit', 
        minute: '2-digit', 
        second: '2-digit',
        hour12: false
      };
    const formattedDate = currentDate.toLocaleString('vi-VN', options);

    try {
        set(ref(db, `DanhGia/${newId}`), {
            MaDanhGia: newId,
            MaNguoiDung: userData.MaNguoiDung,
            MaSanPham: productId,
            DiemDanhGia: rating,
            VanBanDanhGia: review,
            ThoiGianDanhGia: formattedDate,
        })
        return true
    } catch (error) {
        console.log(error)
        return false
    }
}

/**
 * @notice Get review of a product
 * @param productId The id of the product that we want to get review
 */
const getReview = async (productId) => {
    const dbRef = ref(getDatabase())
    try {
        const reviewsSnapshot = await get(child(dbRef, `DanhGia/`))
        const reviews = reviewsSnapshot.val()

        let reviewsProductId = []
        for (const key in reviews) {
            if (reviews[key].MaSanPham === productId) {
                reviewsProductId.push(reviews[key])
            }
        }
        return reviewsProductId
    } catch (error) {
        console.log(error)
        return error
    }
}

export { setReview, getReview }