import { useNavigation } from "@react-navigation/native";
import axios from "axios";
import { Image } from "expo-image";
import React, { useEffect, useState } from "react";
import {
    Pressable,
    ScrollView,
    Text,
    TouchableOpacity,
    View
} from "react-native";
import Draggable from "react-native-draggable";
import * as Icons from "react-native-heroicons/outline";
import Animated, { FadeInUp } from "react-native-reanimated";
import Carousel from "react-native-reanimated-carousel";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import { useSelector } from "react-redux";
import Item from "../components/item";
import ModalLoading from "../components/modalLoading";
import { getBanner } from "../controller/BannerController";
import { getProducts, getProductsBestSeller } from "../controller/ProductController";
import { getUserData } from "../controller/StorageController";
import getDefaultAddress from "../customHooks/getDefaultAddress";
import { colors } from "../theme";
import { blurhash } from "../utils";

const HomeScreen = () => {
    const navigation = useNavigation();
    const [products, setProducts] = useState(null);
    const [bestSeller, setBestSeller] = useState(null);
    const [user, setUser] = useState(null);
    const [banners, setBanners] = useState(null);
    const [proBestSeller, setProBestSeller] = useState([]);
    const [recommendProduct, setRecommendProduct] = useState([])
    const [isLoading, setIsLoading] = useState(false);

    const addressData = getDefaultAddress();

    const cart = useSelector((state) => state.cart.cart);

    const handleGetProducts = async () => {
        setIsLoading(true);
        const listProducts = await getProducts();
        const allProducts = handleSetProduct(listProducts)
        setProducts([...allProducts])
        setIsLoading(false);
    };

    const handleSetProduct = (listProducts) => {
        let allProducts = [];
        if (listProducts) {
            for (const key in listProducts) {
                const productData = {
                    MaSanPham: listProducts[key].MaSanPham,
                    TenSanPham: listProducts[key].TenSanPham,
                    HinhAnh: listProducts[key].HinhAnh,
                    SoLuong: listProducts[key].SoLuong,
                    LoaiSanPham: listProducts[key].LoaiSanPham,
                    PhanTramGiam: listProducts[key].PhanTramGiam,
                    Mota: listProducts[key].Mota,
                    Size: {
                        Nho: {
                            TenKichThuoc:
                                listProducts[key]?.ChiTietKichThuocSanPham
                                    ?.KT0001?.TenKichThuoc,
                            Gia: listProducts[key]?.ChiTietKichThuocSanPham
                                ?.KT0001?.Gia,
                        },
                        Thuong: {
                            TenKichThuoc:
                                listProducts[key]?.ChiTietKichThuocSanPham
                                    ?.KT0002?.TenKichThuoc,
                            Gia: listProducts[key]?.ChiTietKichThuocSanPham
                                ?.KT0002?.Gia,
                        },
                        Lon: {
                            TenKichThuoc:
                                listProducts[key]?.ChiTietKichThuocSanPham
                                    ?.KT0003?.TenKichThuoc,
                            Gia: listProducts[key]?.ChiTietKichThuocSanPham
                                ?.KT0003?.Gia,
                        },
                    },
                };
                allProducts.push(productData);
            }
        }

        // setProducts([...allProducts]);
        return allProducts;
    }

    const handleGetBestSeller = async () => {
        const bestSeller = await getProductsBestSeller();
        setProBestSeller(bestSeller)
    }

    const handleGetRecommend = async () => {
        if (user) {
            const recommend = await axios.post(`https://huynhnhan2003.pythonanywhere.com/recommend`, {
                MaSanPham: 'SP0001',
                MaKhachHang: user?.MaNguoiDung
            })

            const length = recommend.data.length

            let listProducts = []
            for (let i = 0; i < length; ++i) {
                const masp = recommend.data[i].MaSanPham
                listProducts.push(masp)
            }

            setRecommendProduct(listProducts)
        }
    }

    const getUser = async () => {
        try {
            const userData = await getUserData();
            setUser(userData);
        } catch (error) {
            console.log(error);
        }
    };

    const handleGetBanners = async () => {
        const banners = await getBanner();
        setBanners(banners);
    };

    useEffect(() => {
        handleGetProducts();
        getUser();
        handleGetBanners();
        handleGetBestSeller();
    }, []);
    
    useEffect(() => {
        handleGetRecommend();
    }, [user])

    return (
        <View className="flex-1">
            <ScrollView showsVerticalScrollIndicator={false}>
                <View
                    style={{ height: hp(30) }}
                    className="bg-yellow-950 space-y-5"
                >
                    {/* Header */}
                    <View
                        style={{ width: wp(90), marginTop: hp(8) }}
                        className="flex-row justify-between mx-auto items-center"
                    >
                        <View>
                            <View className='flex-row items-end space-x-1'>
                                <Text className="text-white mb-1">Giao đến</Text>
                                <Text className='text-lg text-white font-bold'>{user?.HoTen}</Text>
                            </View>
                            <TouchableOpacity
                                onPress={() => navigation.navigate("Address")}
                            >
                                <Text
                                    className="text-white text-base font-semibold"
                                    style={{ width: wp(70) }}
                                >
                                    {addressData?.DiaChi}
                                </Text>
                            </TouchableOpacity>
                        </View>
                        <View>
                            <Pressable onPress={() => navigation.navigate('Profile')}>
                                <Image
                                    source={{
                                        uri: user?.HinhAnh
                                            ? user?.HinhAnh
                                            : "https://user-images.githubusercontent.com/5709133/50445980-88299a80-0912-11e9-962a-6fd92fd18027.png",
                                    }}
                                    contentFit="cover"
                                    placeholder={{ blurhash }}
                                    style={{ width: hp(8), height: hp(8) }}
                                    transition={1000}
                                    className="rounded-full"
                                />
                            </Pressable>
                        </View>
                    </View>
                </View>

                <View style={{ marginTop: hp(-10) }}>
                    {/* carousel */}
                    {banners && (
                        <Carousel
                            loop
                            width={wp(100)}
                            height={hp(22)}
                            autoPlay={true}
                            data={banners}
                            scrollAnimationDuration={1000}
                            renderItem={({ item }) => (
                                <View className="justify-center items-center">
                                    <Image
                                        source={{ uri: item?.HinhAnh }}
                                        contentFit="cover"
                                        placeholder={{ blurhash }}
                                        transition={1000}
                                        style={{
                                            width: wp(90),
                                            height: hp(20),
                                        }}
                                        className="rounded-lg"
                                    />
                                </View>
                            )}
                        />
                    )}
                </View>

                {/* Flash sale */}
                <View className="mx-5">
                    <View>
                        <View
                            className="p-1 rounded-md mt-2 ml-2"
                            style={{
                                backgroundColor: colors.active,
                                width: wp(61),
                                height: wp(9),
                            }}
                        ></View>
                        <View
                            className="flex-row bg-white rounded-md p-1 items-center absolute space-x-2"
                            style={{ width: wp(60), height: wp(9) }}
                        >
                            <Text
                                className="font-semibold"
                                style={{ color: colors.text(1), fontSize: wp(5) }}
                            >
                                Giảm giá hôm nay
                            </Text>
                            <Image
                                source={require("../assets/images/flashSale.png")}
                                style={{ width: wp(7), height: wp(7) }}
                            />
                        </View>
                    </View>
                </View>

                {/* card item */}
                <View className="mx-5 flex-row flex-wrap justify-between">
                    {products &&
                        products
                            .filter((item) => item.SoLuong > 0 && item.PhanTramGiam > 0)
                            .map((product) => {
                                return (
                                    <Animated.View key={product.MaSanPham} entering={FadeInUp.duration(1500)}>
                                        <Item
                                            product={product}
                                            isSale={true}
                                            isBestSeller={proBestSeller.includes(product.MaSanPham)}
                                        />
                                    </Animated.View>
                                );
                            })}
                </View>

                {/* Best seller */}
                <View className="mx-5 mt-5">
                    <View>
                        <View
                            className="p-1 rounded-md mt-2 ml-2"
                            style={{
                                backgroundColor: colors.active,
                                width: wp(61),
                                height: wp(9),
                            }}
                        ></View>
                        <View
                            className="flex-row bg-white rounded-md p-1 absolute space-x-2"
                            style={{ width: wp(60), height: wp(9) }}
                        >
                            <Text
                                className="text-xl font-semibold"
                                style={{ color: colors.text(1) }}
                            >
                                Sản phẩm bán chạy
                            </Text>
                            <Image
                                source={require("../assets/images/bestSeller.png")}
                                style={{ width: wp(7), height: wp(7) }}
                            />
                        </View>
                    </View>
                </View>

                {/* card item */}
                <View className="mx-5 flex-row flex-wrap justify-between">
                    {products &&
                        products
                            .filter((item) => item.SoLuong > 0)
                            .filter((item) => proBestSeller.includes(item.MaSanPham))
                            .map((product) => {
                                return (
                                    <Animated.View key={product.MaSanPham} entering={FadeInUp.duration(1500)}>
                                        <Item
                                            product={product}
                                            isSale={product.PhanTramGiam > 0}
                                            isBestSeller={true}
                                        />
                                    </Animated.View>
                                );
                            })}
                </View>

                {/* Recommend product */}
                <View className="mx-5 mt-5">
                    <View>
                        <View
                            className="p-1 rounded-md mt-2 ml-2"
                            style={{
                                backgroundColor: colors.active,
                                width: wp(61),
                                height: wp(9),
                            }}
                        ></View>
                        <View
                            className="flex-row bg-white rounded-md p-1 absolute items-center"
                            style={{ width: wp(60), height: wp(9) }}
                        >
                            <Text
                                className="text-xl font-semibold"
                                style={{ color: colors.text(1) }}
                            >
                                Sản phẩm cho bạn
                            </Text>
                            <Image
                                source={require("../assets/images/bestChoice.png")}
                                style={{ width: wp(9), height: wp(9) }}
                            />
                        </View>
                    </View>
                </View>

                {/* card item */}
                <View className="mx-5 flex-row flex-wrap justify-between">
                    {products &&
                        products
                            .filter((item) => item.SoLuong > 0)
                            .filter((item) => recommendProduct.includes(item.MaSanPham))
                            .map((product) => {
                                return (
                                    <Animated.View key={product.MaSanPham} entering={FadeInUp.duration(1500)}>
                                        <Item
                                            product={product}
                                        />
                                    </Animated.View>
                                );
                            })}
                </View>
            </ScrollView>

            {/* cart */}
            <Draggable
                x={wp(80)}
                y={hp(82)}
                minX={wp(5)}
                maxX={wp(95)}
                minY={hp(5)}
                maxY={hp(90)}
                renderSize={24}
                renderColor="amber"
                isCircle
            >
                <View>
                    <TouchableOpacity
                        onPress={() => navigation.navigate("Cart")}
                        className="p-5 bg-yellow-600 rounded-full"
                    >
                        <Icons.ShoppingCartIcon
                            size={30}
                            strokeWidth={2}
                            color={colors.primary}
                        />
                        <View className="absolute -right-1 top-1 bg-red-500 px-2 rounded-full">
                            <Text className="text-white text-base">
                                {cart.length}
                            </Text>
                        </View>
                    </TouchableOpacity>
                </View>
            </Draggable>
            <ModalLoading isLoading={isLoading} />
        </View>
    );
};

export default HomeScreen;
