import { View, Text, ScrollView, TouchableOpacity, TextInput } from "react-native";
import React, { useState } from "react";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import { useNavigation } from "@react-navigation/native";
import * as Icons from "react-native-heroicons/outline";
import { colors } from "../theme";
import { updateInfo } from "../controller/ChangeInfoController";
import ShowToast from "../components/toast";

const ChangeInfoScreen = ({route}) => {
    const {label, content} = route.params;
    const [newInfo, setNewInfo] = useState('');
    const navigation = useNavigation();
    let type = '';

    if (label == 'Họ và tên') {
        type = 'HoTen';
    } else if (label == 'Giới tính') {
        type = 'GioiTinh';
    } else if (label == 'Ngày sinh') {
        type = 'NgaySinh';
    }

    const handleUpdate = async () => {
        const rs = await updateInfo(newInfo, type);
        ShowToast(rs[0] ? "success" : "error", rs[1], rs[0] ? "Vui lòng đăng nhập lại" : "Vui lòng thử lại")
        if (rs[0]) {
            navigation.popToTop();
            navigation.replace('Login')
        }
    }

    return (
        <View className='flex-1'>
            <View>
                <ScrollView showsVerticalScrollIndicator={false}>
                    {/* avatar */}
                    <View style={{ height: hp(25) }} className="bg-yellow-950">
                        <View className="justify-center items-center mt-20 mx-5">
                            <Text className="text-white text-xl font-bold text-center">
                                Đổi thông tin
                            </Text>

                            <TouchableOpacity
                                onPress={() => navigation.goBack()}
                                style={{ position: "absolute", left: 0 }}
                            >
                                <Icons.ChevronLeftIcon
                                    size={30}
                                    color="#ffffff"
                                />
                            </TouchableOpacity>
                        </View>
                        <View
                            className="items-center justify-center"
                            style={{ marginTop: wp(10) }}
                        >
                            <Text className="text-white text-base font-semibold text-center">
                                Thay đổi thông tin cá nhân của bạn
                            </Text>
                        </View>
                    </View>

                    {/* edit password */}
                    <View style={{backgroundColor: '#f2f2f2'}} className='-mt-5 rounded-3xl'>
                        <View className="mx-5">
                            <View className="space-y-1 mt-5">
                                <Text
                                    className="font-semibold text-base"
                                    style={{ color: colors.text(1) }}
                                >
                                    {label}
                                </Text>
                                <View
                                    className="border rounded-lg"
                                    style={{ borderColor: "#9d9d9d" }}
                                >
                                    <Text className='text-base p-3'>{content}</Text>
                                </View>
                            </View>
                            <View className="space-y-1 mt-5">
                                <Text
                                    className="font-semibold text-base"
                                    style={{ color: colors.text(1) }}
                                >
                                    Thông tin mới
                                </Text>
                                <View
                                    className="border rounded-lg"
                                    style={{ borderColor: "#9d9d9d" }}
                                >
                                    <TextInput
                                        value={newInfo}
                                        onChangeText={(text) => setNewInfo(text)}
                                        className="p-3 text-base"
                                        placeholder="Thông tin mới"
                                    />
                                </View>
                            </View>
                        </View>
                    </View>

                    {/* button */}
                    <View className="mt-10 mx-5">
                        <TouchableOpacity onPress={handleUpdate} className='rounded-md' style={{backgroundColor: colors.primary}}>
                            <Text className='py-3 text-center font-semibold text-white text-xl'>Cập nhật</Text>
                        </TouchableOpacity>
                    </View>
                </ScrollView>
            </View>
        </View>
    );
};

export default ChangeInfoScreen;
