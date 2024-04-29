import {
    View,
    Text,
    TouchableOpacity,
    ScrollView,
    Pressable,
} from "react-native";
import React, { useEffect, useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import * as Icons from "react-native-heroicons/outline";
import { useNavigation } from "@react-navigation/native";
import getDefaultAddress from "../customHooks/getDefaultAddress";
import { Divider } from "react-native-paper";
import ItemPayList from "../components/itemPayList";
import { colors } from "../theme";
import { formatPrice } from "../utils";
import { MaterialCommunityIcons } from '@expo/vector-icons';
import { useSelector } from "react-redux";

const PreparePayScreen = () => {
    const navigation = useNavigation();
    const addressData = getDefaultAddress();
    const [totalProduct, setTotalProduct] = useState(0);
    const [transFee, setTransFee] = useState(10000); // 10,000 VND
    const [total, setTotal] = useState(0);
    const cart = useSelector((state) => state.cart.cart);

    const handleTotal = () => {
        let totalProduct = 0;
        cart.forEach((item) => {
            totalProduct += item.SoLuongGioHang * item.Gia;
        });

        setTotal(totalProduct + transFee);

        setTotalProduct(totalProduct);
    }

    useEffect(() => {
        handleTotal();
    }, [totalProduct, transFee])

    return (
        <View className="flex-1">
            <SafeAreaView
                style={{
                    backgroundColor: "#f2f2f2",
                    shadowColor: "#000000",
                    shadowOffset: { width: 0, height: 4 },
                    shadowOpacity: 0.19,
                    shadowRadius: 5.62,
                    elevation: 6,
                }}
            >
                {/* header */}

                <View className="flex-row justify-between items-center mx-5">
                    <TouchableOpacity onPress={() => navigation.goBack()}>
                        <Icons.ChevronLeftIcon size={30} color={"black"} />
                    </TouchableOpacity>
                    <Text className="text-lg font-semibold">
                        Thanh toán
                    </Text>

                    {/* transparent view to adjust the position of the header */}
                    <View>
                        <Icons.HeartIcon size={30} color="transparent" />
                    </View>
                </View>
            </SafeAreaView>

            <ScrollView
                showsVerticalScrollIndicator={false}
                className="pt-2 space-y-3 flex-1"
            >
                <View className='mx-5 space-y-1'>
                    <View className="flex-row items-center gap-3">
                        <Icons.MapPinIcon size={24} color={"red"} />
                        <Text className="text-base">Địa chỉ nhận hàng</Text>
                    </View>
                    <Pressable
                        onPress={() => navigation.navigate("Address")}
                        className="ml-9 space-y-1 flex-row justify-between"
                    >
                        <View style={{ width: wp(70) }}>
                            <View className="flex-row gap-1 items-center">
                                <Text className="text-base">
                                    {addressData?.HoTen}
                                </Text>
                                <Text>|</Text>
                                <Text className="text-base">
                                    {addressData?.SoDienThoai}
                                </Text>
                            </View>
                            <View>
                                <Text>{addressData?.DiaChi}</Text>
                            </View>
                        </View>
                        <View>
                            <Icons.ChevronRightIcon size={24} color={"black"} />
                        </View>
                    </Pressable>
                </View>

                <Divider className='p-1 bg-white'/>

                <View>
                    <ItemPayList />
                </View>

                <Divider className='p-1 bg-white'/>

                <View className='p-3'>
                    <TouchableOpacity className='flex-row justify-between'>
                        <View className='flex-row space-x-1'>
                            <Icons.TicketIcon size={24} color={colors.active} strokeWidth={2} />
                            <Text className='text-base font-semibold'>Voucher giảm giá</Text>
                        </View>
                        <Icons.ChevronRightIcon size={24} color={"black"} />
                    </TouchableOpacity>
                </View>

                <Divider className='p-1 bg-white'/>

                <View className=''>
                    <View className='mx-1 space-y-2 p-2'>
                        <View className='flex-row space-x-1'>
                            <Icons.TruckIcon size={24} color={colors.active} strokeWidth={2}/>
                            <Text style={{color: colors.text(1)}} className='text-base font-semibold'>Thông tin vận chuyển</Text>
                        </View>
                        <Divider />
                        <View className='flex-row items-center justify-between'>
                            <View className='space-y-1'>
                                <Text className='font-bold text-base'>Vận chuyển nhanh</Text>
                                <Text className='font-semibold text-base'>Giao hàng nhanh</Text>
                                <Text className='text-gray-500'>Nhận hàng sau 30 phút</Text>
                            </View>
                            <View>
                                <Text className='text-base'>{formatPrice(transFee)}</Text>
                            </View>
                        </View>
                    </View>
                </View>

                <Divider className='p-1 bg-white'/>

                <View className=''>
                    <View className='mx-1 space-y-2 p-2'>
                        <View className='flex-row space-x-1'>
                            <Icons.CurrencyDollarIcon size={24} color={colors.active} strokeWidth={2}/>
                            <Text className='text-base font-semibold' style={{color: colors.text(1)}}>Phương thức thanh toán</Text>
                        </View>
                        <Divider />
                        <Text className='text-base'>Thanh toán khi nhận hàng</Text>
                    </View>
                </View>

                <Divider className='p-1 bg-white'/>

                <View>
                    <View className='mx-1 space-y-2 p-2'>
                        <View className='flex-row space-x-1'>
                            <MaterialCommunityIcons name="file-document-multiple-outline" size={24} color={colors.active} />
                            <Text className='text-base font-semibold'>Chi tiết thanh toán</Text>
                        </View>
                        <Divider />
                        <View className=''>
                            <View className='flex-row justify-between'>
                                <Text className='text-base'>Tổng tiền hàng: </Text>
                                <Text className='text-base'>{formatPrice(totalProduct)}</Text>
                            </View>
                            <View className='flex-row justify-between'>
                                <Text className='text-base'>Tổng tiền phí vận chuyển: </Text>
                                <Text className='text-base'>{formatPrice(transFee)}</Text>
                            </View>
                            <View className='flex-row justify-between mt-1'>
                                <Text className='text-base font-semibold'>Tổng thanh toán: </Text>
                                <Text className='text-base font-semibold'>{formatPrice(total)}</Text>
                            </View>
                        </View>
                    </View>
                </View>

                <View className='p-2'></View>
            </ScrollView>

            <View className='bg-white rounded-lg justify-center shadow-md' style={{height: hp(10)}}>
                <View className='mx-5 flex-row justify-between items-center'>
                    <View>
                        <Text className='text-sm font-semibold text-gray-700'>Tổng thanh toán</Text>
                        <Text className='text-xl font-bold' style={{color: colors.text(1)}}>{formatPrice(total)}</Text>
                    </View>
                    <View className='rounded-lg' style={{backgroundColor: colors.primary}}>
                        <TouchableOpacity className='p-3 px-5'>
                            <Text className='font-semibold text-xl text-white'>Thanh toán</Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </View>
        </View>
    );
};

export default PreparePayScreen;
