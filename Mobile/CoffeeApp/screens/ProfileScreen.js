import { View, Text, Dimensions, Image, ScrollView, TouchableOpacity, Alert } from "react-native";
import React, { useEffect, useState } from "react";
import Carousel from "react-native-reanimated-carousel";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import * as Icons from "react-native-heroicons/solid";
import MenuItemProfile from "../components/menuItemProfile";
import { useNavigation } from "@react-navigation/native";
import Toast from "react-native-toast-message";
import { useDispatch } from "react-redux";
import { clearCart } from "../redux/slices/cartSlice";
import {getUserData} from "../controller/StorageController";
import { colors } from "../theme";

const width = Dimensions.get("window").width;

const ProfileScreen = () => {
    const navigation = useNavigation();
    const dispatch = useDispatch();
    const [user, setUser] = useState(null);
    
    const menuItems = [
        { icon: "BookmarkIcon", title: "Đã lưu" },
        { icon: "ClipBoardDocumentListIcon", title: "Đơn hàng" },
        { icon: "HeartIcon", title: "Yêu thích" },
    ];

    const getUser = async () => {
        try {
            const userData = await getUserData();
            setUser(userData);
        } catch (error) {
            console.log(error)            
        }
    }

    useEffect(() => {
        getUser()
    }, [])

    const handleLogout = () => {
        Alert.alert(
            "Xác nhận",
            "Bạn có chắc chắn muốn đăng xuất không?",
            [
                { text: "Hủy", style: "cancel" },
                { text: "Đăng xuất", style: 'destructive', onPress: () => {
                    navigation.replace('Login')
                    dispatch(clearCart());
                } },
            ],
            { cancelable: true }
        );
    };

    return (
        <View className="flex-1">
            <ScrollView showsVerticalScrollIndicator={false}>
                {/* avatar */}
                <View style={{ height: hp(25) }} className="bg-yellow-950">
                    <View className="justify-center items-center" style={{marginTop: hp(9)}}>
                        <Text className="text-white text-xl font-bold text-center">Hồ sơ của tôi</Text>
                    </View>
                    <View className="justify-center items-center" style={{marginTop: hp(6)}}>
                        {user?.HinhAnh ? (
                            <Image
                            source={{uri: user?.HinhAnh}}
                            resizeMode="cover"
                            style={{ width: hp(12), height: hp(12) }}
                            className="rounded-full"
                        />
                        ) : (
                            <Image source={require('../assets/images/avtDemo.png')} resizeMode='cover' style={{width: hp(12), height: hp(12)}} className='rounded-full'/>
                        )}
                    </View>
                </View>

                {/* info */}
                <View className="space-y-1" style={{marginTop: wp(15)}}>
                    <View className="flex-row justify-center gap-2 items-center">
                        <Text className="text-lg font-semibold text-center">{user?.HoTen}</Text>
                        <TouchableOpacity
                            onPress={() => navigation.navigate("Edit")}
                            className="p-1 bg-gray-300 rounded-full">
                            <Icons.PencilIcon size={20} color="#000000" />
                        </TouchableOpacity>
                    </View>
                    <Text className="text-center text-gray-500">{user?.Email}</Text>
                    <Text className="text-center text-gray-500">{user?.SoDienThoai}</Text>
                </View>

                {/* menu item */}
                <View className="mx-5">
                    <View className='p-4 border rounded-[14px]' style={{borderColor: '#9d9d9d', marginTop: wp(10)}}>
                        <TouchableOpacity className='flex-row justify-between items-center'>
                            <View className='flex-row items-center gap-5'>
                                <Icons.ClipboardDocumentListIcon size={30} color={colors.active} />
                                <Text className='text-lg font-semibold'>Thông tin đơn hàng</Text>
                            </View>

                            <TouchableOpacity>
                                <Icons.ChevronRightIcon size={30} color={colors.text(0.5)} />
                            </TouchableOpacity>
                        </TouchableOpacity>
                    </View>
                </View>

                {/* logout */}
            </ScrollView>
            <View className='mx-5 m-5'>
                <TouchableOpacity onPress={handleLogout} className="p-4 bg-red-500 rounded-lg">
                    <Text className="text-white text-center text-lg font-semibold">Đăng xuất</Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

export default ProfileScreen;
