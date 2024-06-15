import {
    View,
    Text,
    TouchableOpacity,
    Pressable,
    Image,
} from "react-native";
import {
    widthPercentageToDP as wp,
    heightPercentageToDP as hp,
} from "react-native-responsive-screen";
import React, { useEffect, useState } from "react";
import { useNavigation } from "@react-navigation/native";
import * as IconsSolid from "react-native-heroicons/solid";
import { colors } from "../theme";
import { formatNumber, formatPrice } from "../utils";
import { getReview } from "../controller/ReviewController";
import Animated from "react-native-reanimated";

const Item = ({ product, isSale, isBestSeller }) => {
    const [initialPrice, setInitialPrice] = useState(isSale ? product.Size.Thuong.Gia * (1 - product.PhanTramGiam / 100) : product.Size.Thuong.Gia);
    const [initialSize, setInitialSize] = useState("M");
    const navigation = useNavigation();
    const [size, setSize] = useState(initialSize);
    const [price, setPrice] = useState(formatPrice(initialPrice));
    const [ratingPoint, setRatingPoint] = useState(0);

    let disabledButton = product.SoLuong === 0 ? true : false;

    const handleGetReview = async () => {
        const reviews = await getReview(product.MaSanPham);

        let totalRatingPoint = 0;
        reviews?.forEach((review) => {
            totalRatingPoint += review?.DiemDanhGia;
        });

        setRatingPoint(totalRatingPoint > 0 ? totalRatingPoint / reviews.length : 0);
    };

    useEffect(() => {
        handleGetReview();
    }, []);

    const handleSizeAndPrice = (size) => {
        const discount = product?.PhanTramGiam / 100
        const sizeString = size === "S" ? "Nho" : size === "M" ? "Thuong" : "Lon";
        setSize(size);
        setInitialSize(size);

        if (isSale) {
            setPrice(formatPrice(product?.Size[sizeString]?.Gia * (1 - discount)));
            setInitialPrice(product?.Size[sizeString]?.Gia * (1 - discount));
        } else {
            setPrice(formatPrice(product?.Size[sizeString]?.Gia));
            setInitialPrice(product?.Size[sizeString]?.Gia);
        }
    };

    return (
        <Pressable
            disabled={disabledButton}
            onPress={() =>
                navigation.navigate("Detail", {
                    ...product,
                    initialSize,
                    initialPrice,
                    PhanTramGiam: product.PhanTramGiam,
                })
            }
            className="bg-white rounded-[16px] mt-3 mb-2"
            style={{width: wp(42), height: wp(63)}}
        >
            {isSale ? (
                <View className="z-10 top-2" style={{ left: wp(0.8) }}>
                    <View
                        className="p-1 absolute items-center justify-center flex-row space-x-1"
                        style={{
                            width: wp(25),
                            left: wp(-2.5),
                            height: wp(5.3),
                            borderTopRightRadius: 4,
                            borderBottomRightRadius: 4,
                            backgroundColor: colors.active,
                        }}
                    >
                        <IconsSolid.BoltIcon size={16} color={"#fff"} />
                        <Text className="text-white text-xs font-bold">
                            Giảm {product?.PhanTramGiam}%
                        </Text>
                    </View>
                    <View
                        className="absolute z-50"
                        style={{
                            borderLeftWidth: wp(0.8),
                            borderLeftColor: "transparent",
                            borderRightWidth: wp(0.8),
                            borderRightColor: colors.active,
                            borderTopWidth: wp(0.8),
                            borderTopColor: colors.active,
                            borderBottomWidth: wp(0.8),
                            borderBottomColor: "transparent",
                            left: wp(-2.5),
                            top: wp(5),
                        }}
                    ></View>
                </View>
            ) : null}

            {
                isBestSeller ? (
                    <View className='z-10'>
                        <View className='absolute p-2 rounded-full bg-red-500 top-0 right-0'>
                            <Text className='text-white font-semibold' style={{fontSize: wp(3.2)}}>Bán chạy</Text>
                        </View>
                    </View>
                ) : (
                    null
                )
            }

            <View className="px-1">
                <View
                    className="p-1 justify-center items-center mt-1"
                    style={{ width: wp(40), height: wp(40) }}
                >
                    <Image
                        source={{ uri: product.HinhAnh }}
                        resizeMode="contain"
                        style={{ width: wp(40), height: wp(40) }}
                        className="rounded-[16px]"
                    />

                    <View
                        className="absolute flex-row items-center bottom-0 right-0 p-1"
                        style={{
                            borderTopLeftRadius: 12,
                            borderBottomRightRadius: 16,
                            backgroundColor: "rgba(0,0,0,0.16)",
                        }}
                    >
                        <IconsSolid.StarIcon size={16} color={"#fbbe21"} />
                        <Text className="text-white font-bold p-1">
                            {formatNumber(ratingPoint, 1)}
                        </Text>
                    </View>
                </View>
            </View>

            <View className="px-2 pb-2 space-y-2">
                <Text
                    className="font-bold"
                    numberOfLines={1}
                    style={{ color: colors.text(1), width: wp(36), fontSize: wp(4) }}
                >
                    {product?.TenSanPham}
                </Text>
                <View className="space-y-2">
                    <View className="flex-row justify-between">
                        <TouchableOpacity
                            onPress={() => handleSizeAndPrice("S")}
                            className={
                                "p-1 px-4 rounded-md " +
                                (size === "S" ? "bg-amber-600" : "bg-amber-200")
                            }
                        >
                            <Text>S</Text>
                        </TouchableOpacity>
                        <TouchableOpacity
                            onPress={() => handleSizeAndPrice("M")}
                            className={
                                "p-1 px-4 rounded-md " +
                                (size === "M" ? "bg-amber-600" : "bg-amber-200")
                            }
                        >
                            <Text>M</Text>
                        </TouchableOpacity>
                        <TouchableOpacity
                            onPress={() => handleSizeAndPrice("L")}
                            className={
                                "p-1 px-4 rounded-md " +
                                (size === "L" ? "bg-amber-600" : "bg-amber-200")
                            }
                        >
                            <Text>L</Text>
                        </TouchableOpacity>
                    </View>

                    <View className="flex-row justify-between">
                        <Text
                            className="text-sm font-semibold"
                            style={{ color: colors.text(1) }}
                        >
                            {price}
                        </Text>

                        <View className="flex-row items-center">
                            <Text className="italic text-xs text-gray-400">
                                Số lượng:{" "}
                            </Text>
                            <Text
                                className="text-sm font-semibold"
                                style={{ color: colors.text(1) }}
                            >
                                {product?.SoLuong}
                            </Text>
                        </View>
                    </View>
                </View>
            </View>
            {disabledButton == true ? (
                <View
                    className="absolute top-0 right-0 bottom-0 left-0 rounded-[16px]"
                    style={{ backgroundColor: "rgba(227, 227, 227, 0.3)" }}
                ></View>
            ) : null}
        </Pressable>
    );
};

export default Item;
