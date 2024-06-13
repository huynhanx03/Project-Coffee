import { useNavigation } from "@react-navigation/native";
import React, { useEffect, useState } from "react";
import {
    Dimensions,
    ScrollView,
    Text,
    TouchableOpacity,
    View,
    TextInput,
} from "react-native";
import * as Icons from "react-native-heroicons/solid";
import Button from "../components/button";
import { colors } from "../theme";
import { changePassword } from "../controller/ChangePasswordController";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import ShowToast from "../components/toast";

const width = Dimensions.get("window").width;

const ChangePasswordForgotScreen = () => {
    const navigation = useNavigation();
    const [newPassword, setNewPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [isHide, setIsHide] = useState(true);

    const handleChangePassword = async () => {
        if (newPassword == "" || confirmPassword == "") {
            ShowToast("error", "Thông báo", "Vui lòng nhập đầy đủ thông tin");
            return;
        }
        const rs = await changePassword('', newPassword, confirmPassword, true);
        ShowToast(rs[0] ? "success" : "error", "Thông báo", rs[1])

        if (rs[0]) {
            setTimeout(() => {
                navigation.replace('Login')
            }, 2000);
        }
    }

    return (
        <View className="flex-1">
            <ScrollView showsVerticalScrollIndicator={false}>
                <View style={{ height: hp(25) }} className="bg-yellow-950">
                    <View className="justify-center items-center mt-20 mx-5">
                        <Text className="text-white text-xl font-bold text-center">
                            Đổi mật khẩu
                        </Text>
                    </View>
                    <View
                        className="items-center justify-center"
                        style={{ marginTop: wp(10) }}
                    >
                        <Text className="text-white text-base font-semibold text-center">
                            Đổi mật khẩu mới cho tài khoản của bạn
                        </Text>
                    </View>
                </View>

                {/* edit password */}
                <View className="mx-5">
                    <View className="space-y-1 mt-5">
                        <View className="flex-row space-x-2">
                            <Icons.KeyIcon size={24} color={colors.text(1)} />
                            <Text
                                className="font-semibold text-base"
                                style={{ color: colors.text(1) }}
                            >
                                Mật khẩu mới
                            </Text>
                        </View>

                        <View
                            className="border rounded-lg flex-row justify-between items-center"
                            style={{ borderColor: "#9d9d9d" }}
                        >
                            <TextInput
                                value={newPassword}
                                className="p-3 text-base"
                                style={{ width: "90%" }}
                                placeholder="Mật khẩu mới"
                                secureTextEntry={isHide}
                                onChangeText={(e) => setNewPassword(e)}
                            />

                            <TouchableOpacity
                                onPress={() => setIsHide(!isHide)}
                            >
                                {isHide ? (
                                    <Icons.EyeIcon
                                        size={24}
                                        color={colors.text(1)}
                                        style={{ marginRight: 5 }}
                                    />
                                ) : (
                                    <Icons.EyeSlashIcon
                                        size={24}
                                        color={colors.text(1)}
                                        style={{ marginRight: 5 }}
                                    />
                                )}
                            </TouchableOpacity>
                        </View>
                    </View>

                    <View className="space-y-1 mt-5">
                        <View className="flex-row space-x-2">
                            <Icons.KeyIcon size={24} color={colors.text(1)} />
                            <Text
                                className="font-semibold text-base"
                                style={{ color: colors.text(1) }}
                            >
                                Xác nhận mật khẩu
                            </Text>
                        </View>

                        <View
                            className="border rounded-lg flex-row justify-between items-center"
                            style={{ borderColor: "#9d9d9d" }}
                        >
                            <TextInput
                                value={confirmPassword}
                                className="p-3 text-base"
                                style={{ width: "90%" }}
                                placeholder="Xác nhận mật khẩu"
                                secureTextEntry={isHide}
                                onChangeText={(e) => setConfirmPassword(e)}
                            />

                            <TouchableOpacity
                                onPress={() => setIsHide(!isHide)}
                            >
                                {isHide ? (
                                    <Icons.EyeIcon
                                        size={24}
                                        color={colors.text(1)}
                                        style={{ marginRight: 5 }}
                                    />
                                ) : (
                                    <Icons.EyeSlashIcon
                                        size={24}
                                        color={colors.text(1)}
                                        style={{ marginRight: 5 }}
                                    />
                                )}
                            </TouchableOpacity>
                        </View>
                    </View>
                </View>

                {/* button */}
                <View className="mt-10 mx-5">
                    <Button
                        content="Cập nhật mật khẩu"
                        handle={handleChangePassword}
                    />
                </View>
            </ScrollView>
        </View>
    );
};

export default ChangePasswordForgotScreen;
