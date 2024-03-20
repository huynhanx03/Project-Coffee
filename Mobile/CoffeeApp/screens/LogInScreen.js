import { View, Text, Image, Dimensions, Pressable, TouchableOpacity } from "react-native";
import React, { useState } from "react";
import { colors } from "../theme";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import Button from "../components/button";
import { useNavigation } from "@react-navigation/native";
import { TextInput } from "react-native-paper";

const { width, height } = Dimensions.get("window");

export default function LogInScreen() {
    const navigation = useNavigation();
    const [isHide, setIsHide] = useState(true);
    const handleShowPassword = () => {
        setIsHide(!isHide);
    };
    return (
        <View className="flex-1 justify-center items-center">
            <Image
                className="relative"
                source={require("../assets/images/bgWelcome.png")}
                style={{ width: width, height: height }}
            />
            <View className="absolute top-48">
                <Text className="text-4xl font-extrabold text-center mb-10" style-={{ color: colors.primary }}>
                    Chào mừng!
                </Text>
                <View className="space-y-3" style={{ width: wp(90) }}>
                    <TextInput mode="outlined" label="Tên đăng nhập" activeOutlineColor={colors.primary} />
                    <TextInput
                        mode="outlined"
                        label="Mật khẩu"
                        secureTextEntry={isHide}
                        activeOutlineColor={colors.primary}
                        right={<TextInput.Icon icon={isHide ? "eye" : "eye-off"} onPress={handleShowPassword} />}
                    />

                    <TouchableOpacity className="mb-3" onPress={() => navigation.navigate("Forgot")}>
                        <Text className="text-right font-bold text-base" style={{ color: colors.active }}>
                            Quên mật khẩu?
                        </Text>
                    </TouchableOpacity>

                    <Button content="Đăng nhập" handle={() => navigation.navigate('Home')} />
                </View>

                <View className="mt-14 flex-row justify-between items-center" style={{ width: wp(90) }}>
                    <Text
                        style={{
                            height: 1,
                            borderColor: "rgba(59, 29, 12, 0.4)",
                            borderWidth: 1,
                            width: wp(25),
                        }}></Text>

                    <Text style={{ color: "#8B6122" }}>Hoặc đăng nhập với</Text>

                    <Text
                        style={{
                            height: 1,
                            borderColor: "rgba(59, 29, 12, 0.4)",
                            borderWidth: 1,
                            width: wp(25),
                        }}></Text>
                </View>

                <TouchableOpacity
                    className="flex justify-center items-center bg-white border-[1px] rounded-lg py-1 mt-5"
                    style={{ borderColor: "#E8ECF4" }}>
                    <Image source={require("../assets/icons/ggIcon.png")} />
                </TouchableOpacity>
            </View>
            <View className="flex flex-row justify-center items-center mt-20 absolute bottom-10">
                <Text className="font-semibold">Bạn chưa có tài khoản? </Text>
                <TouchableOpacity onPress={() => navigation.navigate("Register")}>
                    <Text className="font-semibold" style={{ color: colors.active }}>
                        Đăng ký tại đây!
                    </Text>
                </TouchableOpacity>
            </View>
        </View>
    );
}
