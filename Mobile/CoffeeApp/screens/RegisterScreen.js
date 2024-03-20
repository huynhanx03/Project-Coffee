import { View, Text, Dimensions, TouchableOpacity, Image } from "react-native";
import React, { useState } from "react";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import Button from "../components/button";
import { colors } from "../theme";
import { useNavigation } from "@react-navigation/native";
import { TextInput } from "react-native-paper";

const { width, height } = Dimensions.get("window");

const RegisterScreen = () => {
    const navigation = useNavigation();
    const [isHide, setIsHide] = useState(true);

    const handleShowPassword = () => {
        setIsHide(!isHide);
    };
    return (
        <View className="flex-1 justify-center items-center">
            <Image
                className="relative"
                source={require("../assets/images/bgOther.png")}
                style={{ width: width, height: height }}
            />
            <View className="absolute top-36">
                <Text className="text-4xl font-extrabold text-center mb-20" style-={{ color: colors.primary }}>
                    Chào bạn mới!
                </Text>
                <View className="space-y-3" style={{ width: wp(90) }}>
                    <TextInput mode="outlined" label="Tên đăng nhập" activeOutlineColor={colors.primary} />
                    <TextInput mode="outlined" label="Email" activeOutlineColor={colors.primary} />
                    <TextInput
                        mode="outlined"
                        label="Mật khẩu"
                        secureTextEntry={isHide}
                        activeOutlineColor='#3b1d0c'
                        right={<TextInput.Icon icon={isHide ? "eye" : "eye-off"} onPress={handleShowPassword} />}
                    />
                    <TextInput
                        mode="outlined"
                        label="Nhập lại mật khẩu"
                        secureTextEntry={isHide}
                        activeOutlineColor={colors.primary}
                        right={<TextInput.Icon icon={isHide ? "eye" : "eye-off"} onPress={handleShowPassword} />}
                        className='mb-3'
                    />

                    <Button content='Đăng ký' handle={() =>{}}/>
                </View>

                <View className="mt-10 flex-row justify-between items-center" style={{ width: wp(90) }}>
                    <Text
                        style={{
                            height: 1,
                            borderColor: "rgba(59, 29, 12, 0.4)",
                            borderWidth: 1,
                            width: wp(25),
                        }}></Text>

                    <Text style={{ color: "#8B6122" }}>Hoặc đăng ký với</Text>

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
            <View className="flex flex-row justify-center items-center absolute bottom-10">
                <Text className="font-semibold">Đã có tài khoản? </Text>
                <TouchableOpacity onPress={() => navigation.goBack()}>
                    <Text className="font-semibold" style={{ color: colors.active }}>
                        Đăng nhập tại đây!
                    </Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

export default RegisterScreen;
