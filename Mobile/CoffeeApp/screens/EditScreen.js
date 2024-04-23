import { View, Text, Dimensions, Image, ScrollView, TouchableOpacity } from "react-native";
import React from "react";
import Carousel from "react-native-reanimated-carousel";
import { widthPercentageToDP as wp, heightPercentageToDP as hp } from "react-native-responsive-screen";
import * as Icons from "react-native-heroicons/solid";
import MenuItemProfile from "../components/menuItemProfile";
import { useNavigation } from "@react-navigation/native";
import InputCustom from "../components/inputCustom";
import { colors } from "../theme";


const width = Dimensions.get("window").width;

const EditScreen = () => {
    const navigation = useNavigation();
    const handleClick = () => {
        navigation.navigate('ChangePassword');
    }
    return (
        <View className='flex-1'>
            <ScrollView>

                {/* avatar */}
                <View style={{height: hp(25)}} className='bg-yellow-950'>
                    <View className='justify-center items-center mt-20'>
                        <Text className='text-white text-xl font-bold text-center'>Hồ sơ của tôi</Text>
                    </View>
                    <View className='justify-center items-center mt-16'>
                        <Image source={require('../assets/images/avtDemo.png')} resizeMode="contain" style={{width: hp(12), height: hp(12)}}/>
                    </View>
                </View>

                {/* edit avatar */}
                <View className='mt-16 items-center'>
                    <TouchableOpacity className='p-3 bg-yellow-950 rounded-lg'>
                        <Text className='text-white font-semibold'>Đổi ảnh đại diện</Text>
                    </TouchableOpacity>
                </View>



                {/* Account info */}
                <View className='mt-5 p-2 px-5 bg-slate-200'>
                    <Text className='text-base font-semibold' style={{color: 'gray'}}>Thông tin tài khoản</Text>
                </View>
                <View className='mx-5 mt-1'>
                    <InputCustom lable={'Tên đăng nhập'}/>
                    <InputCustom lable={'Email'}/>
                    <InputCustom lable={'Số điện thoại'}/>

                    <MenuItemProfile icon='Key' title='Đổi mật khẩu' handleClick={handleClick}/>
                </View>
            </ScrollView>
        </View>
    );
};

export default EditScreen;