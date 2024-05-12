import Toast from "react-native-toast-message";

const ShowToast = (type, message1, message2) => {
    Toast.show({
        type: type,
        topOffset: 70,
        text1: message1,
        text2: message2,
        text1Style: { fontSize: 18 },
        text2Style: { fontSize: 15 },
        visibilityTime: 2000,
    });
};

export default ShowToast