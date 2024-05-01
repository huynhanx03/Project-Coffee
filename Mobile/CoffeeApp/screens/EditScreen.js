import {
    View,
    Text,
    Dimensions,
    Image,
    ScrollView,
    TouchableOpacity,
} from "react-native";
import React, { useEffect, useState } from "react";
import Carousel from "react-native-reanimated-carousel";
import {
    widthPercentageToDP as wp,
    heightPercentageToDP as hp,
} from "react-native-responsive-screen";
import * as Icons from "react-native-heroicons/solid";
import MenuItemProfile from "../components/menuItemProfile";
import { useNavigation } from "@react-navigation/native";
import InputCustom from "../components/inputCustom";
import { colors } from "../theme";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { set } from "firebase/database";
import {
    getUserData,
    removeUserData,
    storeData,
} from "../controller/StorageController";
import * as ImagePicker from "expo-image-picker";
import { Cloudinary, upload } from "@cloudinary/url-gen";
import { uploadImage } from "../controller/UploadAvatarController";

const width = Dimensions.get("window").width;

const EditScreen = () => {
    const navigation = useNavigation();
    const [user, setUser] = useState(null);

    const handleClick = () => {
        navigation.navigate("ChangePassword");
    };

    const getUser = async () => {
        try {
            const user = await getUserData();
            setUser(user);
        } catch (error) {
            console.log(error);
        }
    };

    useEffect(() => {
        getUser();
    }, []);

    const handleUploadImage = (image) => {
        const data = new FormData();
        data.append("file", image);
        data.append("upload_preset", "CoffeeAvatar");
        data.append("cloud_name", "dev9hnuhw");

        fetch("https://api.cloudinary.com/v1_1/dev9hnuhw/image/upload", {
            method: "post",
            body: data,
        })
            .then((res) => res.json())
            .then((data) => {
                uploadImage(data.url);
            });
    };

    const [image, setImage] = useState(null);

    const pickImage = async () => {
        // No permissions request is necessary for launching the image library
        let result = await ImagePicker.launchImageLibraryAsync({
            mediaTypes: ImagePicker.MediaTypeOptions.All,
            allowsEditing: true,
            aspect: [1, 1],
            quality: 1,
        });

        // console.log(result.assets[0].uri);

        if (!result.canceled) {
            // setImage(result.assets[0].uri);
            let newFile = {
                uri: result.assets[0].uri,
                type: `test/${result.assets[0].uri.split(".")[1]}`,
                name: `test.${result.assets[0].uri.split(".")[1]}`,
            };
            handleUploadImage(newFile);
        }
    };

    return (
        <View className="flex-1">
            <ScrollView showsVerticalScrollIndicator={false}>
                {/* avatar */}
                <View style={{ height: hp(25) }} className="bg-yellow-950">
                    <View
                        className="justify-center items-center"
                        style={{ marginTop: hp(9) }}
                    >
                        <Text className="text-white text-xl font-bold text-center">
                            Chỉnh sửa hồ sơ
                        </Text>

                        <Pressable
                            onPress={() => navigation.goBack()}
                            style={{ position: "absolute", left: wp(5) }}
                        >
                            <Icons.ChevronLeftIcon size={24} color="white" />
                        </Pressable>
                    </View>
                    <View
                        className="justify-center items-center"
                        style={{ marginTop: hp(6) }}
                    >
                        <Image
                            source={{ uri: user?.HinhAnh }}
                            resizeMode="cover"
                            style={{ width: hp(12), height: hp(12) }}
                            className="rounded-full"
                        />
                    </View>
                </View>

                {/* edit avatar */}
                <View className="mt-16 items-center">
                    <TouchableOpacity
                        onPress={pickImage}
                        className="p-3 bg-yellow-950 rounded-lg"
                    >
                        <Text className="text-white font-semibold">
                            Đổi ảnh đại diện
                        </Text>
                    </TouchableOpacity>
                </View>

                {/* Account info */}
                <View className="mt-5 p-2 px-5 bg-slate-200">
                    <Text
                        className="text-base font-semibold"
                        style={{ color: "gray" }}
                    >
                        Thông tin tài khoản
                    </Text>
                </View>
                <View className="mx-5 mt-1">
                    <InputCustom lable={"Họ và tên"} content={user?.HoTen} />
                    <InputCustom lable={"Giới tính"} content={user?.GioiTinh} />
                    <InputCustom lable={"Ngày sinh"} content={user?.NgaySinh} />
                    <InputCustom
                        lable={"Tên đăng nhập"}
                        content={user?.TaiKhoan}
                    />
                    <InputCustom lable={"Email"} content={user?.Email} />
                    <InputCustom
                        lable={"Số điện thoại"}
                        content={user?.SoDienThoai}
                    />
                    <InputCustom
                        lable={"Ngày tham gia"}
                        content={user?.NgayTao}
                    />

                    <MenuItemProfile
                        icon="Key"
                        title="Đổi mật khẩu"
                        handleClick={handleClick}
                    />
                </View>

                <View className="mt-10"></View>
            </ScrollView>
        </View>
    );
};

export default EditScreen;
