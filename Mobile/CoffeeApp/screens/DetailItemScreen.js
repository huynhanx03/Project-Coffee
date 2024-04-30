import { View, Text, Image, TouchableOpacity, ScrollView, TextInput, KeyboardAvoidingView, Platform } from "react-native";
import React, { useEffect, useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import * as Icons from "react-native-heroicons/solid";
import { colors } from "../theme";
import { Divider } from "react-native-paper";
import { useNavigation } from "@react-navigation/native";
import CustomeKeyboard from "../components/customKeyboard";
import { heightPercentageToDP as hp, widthPercentageToDP as wp } from "react-native-responsive-screen";
import Button from "../components/button";
import { formatPrice } from "../utils";
import Toast from "react-native-toast-message";
import { useDispatch, useSelector } from "react-redux";
import { addToCart } from "../redux/slices/cartSlice";
import { setCart } from "../controller/CartController";
const ios = Platform.OS === "ios";

const DetailItemScreen = ({route}) => {
    const navigation = useNavigation();
    const dispatch = useDispatch();
    const product = route.params;

    const initialPrice = (product.initialPrice);
    const initialSize = (product.initialSize);

    const [size, setSize] = useState(initialSize);
    const [quantity, setQuantity] = useState(1);
    const [price, setPrice] = useState(initialPrice);
    const [total, setTotal] = useState(initialPrice);
    const [note, setNote] = useState('')
    const [isFavorite, setIsFavorite] = useState(false)

    const cart = useSelector(state => state.cart.cart);

    const handleSizeAndPrice = (size) => {
        setSize(size);
        if (size === 'S') {
            setPrice((product.Size.Nho.Gia))
        } else if (size === 'M') {
            setPrice((product.Size.Thuong.Gia))
        } else {
            setPrice((product.Size.Lon.Gia))
        }
    }

    const handleTotal = () => {
        setTotal(formatPrice(parseInt(price) * quantity));
    }

    useEffect(() => {
        handleTotal();
    }, [price, quantity])

    const handleIncreaseQuantity = () => {
        if (quantity < product.SoLuong) {
            setQuantity(quantity => quantity + 1)
        } else {
            setQuantity(product.SoLuong);
            Toast.show({
                type: 'error',
                text1: 'Lỗi',
                text2: 'Số lượng sản phẩm không đủ',
                topOffset: 70,
                text1Style: {fontSize: 18},
                text2Style: {fontSize: 15},
                visibilityTime: 2000
            })
        }
    }

    const handleDecreaseQuantity = () => {
        if (quantity > 1) {
            setQuantity(quantity => quantity - 1)
        } else {
            Toast.show({
                type: 'error',
                text1: 'Lỗi',
                text2: 'Số lượng sản phẩm phải lớn hơn 0',
                topOffset: 70,
                text1Style: {fontSize: 18},
                text2Style: {fontSize: 15},
                visibilityTime: 2000,
            })
        }
    }

    const handleAddCart = (item) => {
        const itemCart = {
            TenSanPham: item.TenSanPham,
            Gia: item.initialPrice,
            HinhAnh: item.HinhAnh,
            KichThuoc: item.initialSize,
            MaSanPham: item.MaSanPham,
            SoLuongGioHang: item.quantity,
            SoLuong: item.SoLuong
        }
        dispatch(addToCart(itemCart));
        setCart(itemCart)
        Toast.show({
            type: 'success',
            text1: 'Thông báo',
            text2: 'Thêm vào giỏ hàng thành công',
            topOffset: 70,
            text1Style: {fontSize: 18},
            text2Style: {fontSize: 15},
            visibilityTime: 2000
        })
    }
    return (
        <KeyboardAvoidingView  behavior={ios ? "padding" : "height"} style={{ flex: 1 }} keyboardVerticalOffset={0}>

        <View className='flex-1'>
            <SafeAreaView
                style={{
                    backgroundColor: "#f2f2f2",
                    shadowColor: "#000000",
                    shadowOffset: { width: 0, height: 4 },
                    shadowOpacity: 0.19,
                    shadowRadius: 5.62,
                    elevation: 6,
                }}>
                {/* header */}

                <View className="flex-row justify-between items-center mx-5">
                    <TouchableOpacity onPress={() => navigation.goBack()}>
                        <Icons.ChevronLeftIcon size={30} color={"black"}/>
                    </TouchableOpacity>
                    <Text className="text-lg font-semibold">Chi tiết sản phẩm</Text>

                    <TouchableOpacity onPress={() => setIsFavorite(!isFavorite)}>
                        <Icons.HeartIcon size={30} color={isFavorite ? colors.active : "gray"} />
                    </TouchableOpacity>
                </View>
            </SafeAreaView>

            <ScrollView className='mx-5 pt-1 space-y-3 flex-[6]' showsVerticalScrollIndicator={false}>
                {/* image */}
                <Image source={{uri: product.HinhAnh}} resizeMode="cover" style={{ width: '100%', height: 300, borderRadius: 16 }} />

                {/* info */}
                <View className='flex-row justify-between'>
                    <Text style={{color: colors.text(1)}} className='font-semibold text-2xl'>{product.TenSanPham}</Text>
                    <Text style={{color: colors.text(1)}} className='font-semibold text-2xl'>{formatPrice(price)}</Text>
                </View>

                {/* description */}
                <View>
                    <Text numberOfLines={2}>
                        Lorem ipsum dolor sit amet consectetur adipisicing elit. Adipisci magnam eius hic quis omnis alias sequi, a natus aspernatur fuga, accusantium itaque cupiditate nostrum architecto magni eos ut voluptatum. Nostrum!
                    </Text>
                </View>

                {/* star */}
                <View className='flex-row items-center space-x-5'>
                    <Icons.StarIcon size={24} color={'#fbbe21'} />
                    <Text>4.8/5</Text>
                </View>

                <Divider />
                {/* size */}
                <View className='space-y-1'>
                    <Text className='text-base font-semibold' style={{color: colors.text(1)}}>Kích cỡ</Text>

                    <View className='flex-row justify-between'>
                        <TouchableOpacity onPress={() => handleSizeAndPrice('S')} className='rounded-xl border' style={{borderColor: size === 'S' ? colors.active : '#dedede', backgroundColor: size === 'S' ? '#fff5ee' : '#f2f2f2'}}>
                            <Text className='p-2 px-14 py-5'>S</Text>
                        </TouchableOpacity>
                        <TouchableOpacity onPress={() => handleSizeAndPrice('M')} className='rounded-xl border' style={{borderColor: size === 'M' ? colors.active : '#dedede', backgroundColor: size === 'M' ? '#fff5ee' : '#f2f2f2'}}>
                            <Text className='p-2 px-14 py-5'>M</Text>
                        </TouchableOpacity>
                        <TouchableOpacity onPress={() => handleSizeAndPrice('L')} className='rounded-xl border' style={{borderColor: size === 'L' ? colors.active : '#dedede', backgroundColor: size === 'L' ? '#fff5ee' : '#f2f2f2'}}>
                            <Text className='p-2 px-14 py-5'>L</Text>
                        </TouchableOpacity>
                    </View>
                </View>

                {/* quantity */}
                <View className='space-y-1'>
                    <Text className='text-base font-semibold' style={{color: colors.text(1)}}>Số lượng</Text>

                    <View className='flex-row justify-center space-x-4 items-center'>
                        <TouchableOpacity onPress={handleDecreaseQuantity} className='rounded-md' style={{backgroundColor: colors.primary}}>
                            <Text className='px-4 py-2 text-white text-base font-semibold'>-</Text>
                        </TouchableOpacity>

                        <View className='bg-white border border-neutral-400 rounded-md'>
                            <Text className='text-base font-semibold px-4 py-2'>{quantity}</Text>
                        </View>

                        <TouchableOpacity onPress={handleIncreaseQuantity} className='rounded-md' style={{backgroundColor: colors.primary}}>
                            <Text className='px-4 py-2 text-white text-base font-semibold'>+</Text>
                        </TouchableOpacity>
                    </View>
                </View>

                {/* note */}
                
                <View className='space-y-1'>
                    <View className='flex-row items-center'>
                        <Text className='text-base font-semibold' style={{color: colors.text(1)}}>Ghi chú </Text>
                        <Text className='italic text-sm'>(không bắt buộc)</Text>
                    </View>
                    <TextInput multiline={true} placeholder="Ghi chú" className='mb-10 text-base h-20 rounded-lg p-2 border border-gray-400'/>
                </View>

            </ScrollView>
            {/* add to cart */}
            <View className='bg-white flex-1 rounded-lg justify-center shadow-md'>
                <View className='mx-5 flex-row justify-between items-center'>
                    <View>
                        <Text className='text-sm font-semibold text-gray-700'>Tổng</Text>
                        <Text className='text-xl font-semibold'>{total}</Text>
                    </View>
                    <View>
                        <TouchableOpacity onPress={() => handleAddCart({...product, quantity})} className='flex-row bg-amber-400 rounded-lg p-3 px-10 items-center justify-center'>
                            <Icons.ShoppingCartIcon size={30} color={colors.primary} />
                        </TouchableOpacity>
                    </View>
                </View>
            </View>
        </View>
        </KeyboardAvoidingView>
    );
};

export default DetailItemScreen;
