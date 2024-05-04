import { child, get, getDatabase, ref } from "firebase/database"

/**
 * @notice Get user by id
 * @param userId The id of the user
 * @returns user object
 */
const getUserById = async (userId) => {
    const dbRef = ref(getDatabase())
    try {
        const userSnapshot = await get(child(dbRef, `NguoiDung/${userId}`))
        const user = userSnapshot.val()

        return user
    } catch (error) {
        console.log(error)
        return error
    }
}

export { getUserById }