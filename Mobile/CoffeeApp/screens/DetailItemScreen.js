import { View, Text, Image, TouchableOpacity, ScrollView, TextInput, KeyboardAvoidingView, Platform } from "react-native";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import * as Icons from "react-native-heroicons/solid";
import { colors } from "../theme";
import { Divider } from "react-native-paper";
import { useNavigation } from "@react-navigation/native";
import CustomeKeyboard from "../components/customKeyboard";
import Button from "../components/button";
const ios = Platform.OS === "ios";

const DetailItemScreen = () => {
    const navigation = useNavigation();
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

                    <TouchableOpacity>
                        <Icons.HeartIcon size={30} color={"red"} />
                    </TouchableOpacity>
                </View>
            </SafeAreaView>

            <ScrollView className='mx-5 pt-1 space-y-3 flex-[6]' showsVerticalScrollIndicator={false}>
                {/* image */}
                <Image source={require('../assets/images/coffeeDemo.png')} resizeMode="cover" style={{ width: '100%', height: 300, borderRadius: 16 }} />

                {/* info */}
                <View className='flex-row justify-between'>
                    <Text style={{color: colors.text(1)}} className='font-semibold text-2xl'>Espresso</Text>
                    <Text style={{color: colors.text(1)}} className='font-semibold text-2xl'>15.000đ</Text>
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
                        <TouchableOpacity className='rounded-xl border' style={{borderColor: '#dedede'}}>
                            <Text className='p-2 px-14 py-5'>S</Text>
                        </TouchableOpacity>
                        <TouchableOpacity className='rounded-xl border' style={{borderColor: colors.active, backgroundColor: '#fff5ee'}}>
                            <Text className='p-2 px-14 py-5'>M</Text>
                        </TouchableOpacity>
                        <TouchableOpacity className='rounded-xl border' style={{borderColor: '#dedede'}}>
                            <Text className='p-2 px-14 py-5'>L</Text>
                        </TouchableOpacity>
                    </View>
                </View>

                {/* quantity */}
                <View className='space-y-1'>
                    <Text className='text-base font-semibold' style={{color: colors.text(1)}}>Số lượng</Text>

                    <View className='flex-row justify-center space-x-4 items-center'>
                        <TouchableOpacity className='rounded-md' style={{backgroundColor: colors.primary}}>
                            <Text className='px-4 py-2 text-white text-base font-semibold'>-</Text>
                        </TouchableOpacity>

                        <View className='bg-white border border-neutral-400 rounded-md'>
                            <Text className='text-base font-semibold px-4 py-2'>1</Text>
                        </View>

                        <TouchableOpacity className='rounded-md' style={{backgroundColor: colors.primary}}>
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
            <View className='bg-white flex-1 rounded-lg justify-center'>
                <View className='mx-5 flex-row justify-between items-center'>
                    <View>
                        <Text className='text-sm font-semibold text-gray-700'>Tổng</Text>
                        <Text className='text-xl font-semibold'>15.000đ</Text>
                    </View>
                    <View className='flex-row space-x-1'>
                        {/* <TouchableOpacity className='bg-white rounded-lg p-3 mt-3'>
                            <Text className='text-center text-base font-semibold'>Thêm vào giỏ hàng</Text>
                        </TouchableOpacity> */}
                        <Button content='Mua ngay' onPress={() => {}}/>

                        <TouchableOpacity className='bg-amber-400 rounded-lg p-3 items-center justify-center'>
                            <Icons.ShoppingCartIcon size={24} color={colors.primary} />
                        </TouchableOpacity>
                    </View>
                </View>
            </View>
        </View>
        </KeyboardAvoidingView>
    );
};

export default DetailItemScreen;
