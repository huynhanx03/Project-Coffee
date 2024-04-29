import AsyncStorage from "@react-native-async-storage/async-storage"

/**
 * @notice Get user data from AsyncStorage
 * @returns The user data that is logining
 */
const getUserData = async () => {
    try {
        const jsonValue = await AsyncStorage.getItem('user')
        return jsonValue != null ? JSON.parse(jsonValue) : null
    } catch (error) {
        console.log(error)
        return error
    }
}

export default getUserData