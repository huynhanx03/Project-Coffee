import { View, Text, Image, TouchableOpacity } from "react-native";
import React from "react";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import { colors } from "../theme";
import { useDispatch, useSelector } from "react-redux";
import { removeVoucher, setVoucher } from "../redux/slices/voucherSlice";
import { useNavigation } from "@react-navigation/native";

const ItemVoucher = (props) => {
    const voucher = props.voucher;
    const navigation = useNavigation()
    const dispatch = useDispatch()
    const colorsArr = ['#e6be3e', '#60e63e', '#3ee6de', '#2d50ed', '#bb3bed', '#e63088']
    const randomColor = colorsArr[Math.floor(Math.random() * colorsArr.length)]
    const voucherRedux = useSelector(state => state.voucher.voucher)

    const handleUseVoucher = () => {
        if (voucherRedux.length > 0) {
            dispatch(removeVoucher())
        }
        dispatch(setVoucher(voucher))
        navigation.goBack()
    }

    return (
        <View className='flex-1 mt-5 mx-2 shadow-md'>
            <View className='bg-white rounded-xl'>
                <View className='rounded-t-xl' style={{backgroundColor: randomColor}}>
                    <View className='flex-row items-center space-x-4 mx-3' style={{height: wp(18)}}>
                        <View className='p-1 rounded-full border-amber-500 bg-white border-2'>
                            <Image source={require('../assets/images/logo.png')} resizeMode="contain" style={{width: wp(10), height: wp(10)}}/>
                        </View>
                        <Text className='text-lg font-semibold' style={{width: wp(80)}}>{voucher.NoiDung}</Text>
                    </View>
                </View>

                <View className='flex-row items-center space-x-5'>
                    <View className='mx-3 p-5 space-y-5'>
                        <View className='flex-row items-end space-x-2'>
                            <Text className='text-xl font-bold mb-[2px]' style={{color: colors.active}}>GIẢM </Text>
                            <Text className='text-4xl font-bold' style={{color: colors.active}}>{voucher.PhanTramGiam}%</Text>
                        </View>
                        <View className=''>
                            <Text className='text-gray-400'>Sử dụng đến: {voucher.NgayHetHan}</Text>
                        </View>
                    </View>

                    <View>
                        <TouchableOpacity onPress={handleUseVoucher} className='bg-orange-400 rounded-full'>
                            <Text className='font-semibold text-base px-3 py-4'>Sử dụng ngay</Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </View>
        </View>
    );
};

export default ItemVoucher;
