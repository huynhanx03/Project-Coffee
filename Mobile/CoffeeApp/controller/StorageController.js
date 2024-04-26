import AsyncStorage from "@react-native-async-storage/async-storage"

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