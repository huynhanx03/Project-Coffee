import { View, Text } from "react-native";
import React, { useEffect } from "react";
import LottieView from "lottie-react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import { useNavigation } from "@react-navigation/native";

const OrderSuccessScreen = () => {
    const navigation = useNavigation()

    useEffect(() => {
        setTimeout(() => {
            navigation.popToTop();
            navigation.replace('HomeTab')
        }, 3000);
    })

    return (
        <SafeAreaView className="flex-1 bg-white">
            <LottieView
                source={require("../assets/lottie/OrderSuccess.json")}
                style={{
                    height: hp(50),
                    width: wp(80),
                    alignSelf: "center",
                    marginTop: 50,
                    justifyContent: "center",
                }}
                autoPlay
                loop={true}
                speed={0.7}
            />
            <Text
                style={{
                    marginTop: 10,
                    fontSize: wp(5),
                    fontWeight: "600",
                    textAlign: "center",
                }}
            >
                Đơn hàng của bạn đã được ghi nhận
            </Text>
        </SafeAreaView>
    );
};

export default OrderSuccessScreen;
