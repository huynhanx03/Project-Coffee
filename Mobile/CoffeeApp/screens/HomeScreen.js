import { useNavigation } from "@react-navigation/native";
import React, { useEffect, useRef, useState } from "react";
import {
    Image,
    ScrollView,
    Text,
    TextInput,
    TouchableOpacity,
    View,
} from "react-native";
import Draggable from "react-native-draggable";
import * as Icons from "react-native-heroicons/outline";
import Carousel from "react-native-reanimated-carousel";
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import Categories from "../components/categories";
import Item from "../components/item";
import getDefaultAddress from "../customHooks/getDefaultAddress";
import { colors } from "../theme";
import { getCategories, getProducts } from "../controller/ProductController";
import { useDispatch, useSelector } from "react-redux";
import { getCart } from "../controller/CartController";
import { addToCartFromDatabase } from "../redux/slices/cartSlice";
import getUserData from "../controller/StorageController";

const HomeScreen = () => {
    const navigation = useNavigation();
    const dispatch = useDispatch();
    const [isActive, setIsActive] = useState("Tất cả");
    const [products, setProducts] = useState(null);
    const [categories, setCategories] = useState(null);

    const addressData = getDefaultAddress();

    const cart = useSelector((state) => state.cart.cart);

    const handleGetCategories = async () => {
        const listCategories = await getCategories();
        if (listCategories) {
            let allCategories = [];
            for (const key in listCategories) {
                allCategories.push(listCategories[key].LoaiSanPham);
            }
            setCategories(["Tất cả", ...allCategories]);
        }
    };

    const handleGetProducts = async () => {
        let allProducts = [];
        const listProducts = await getProducts();
        if (listProducts) {
            for (const key in listProducts) {
                const productData = {
                    MaSanPham: listProducts[key].MaSanPham,
                    TenSanPham: listProducts[key].TenSanPham,
                    HinhAnh: listProducts[key].HinhAnh,
                    SoLuong: listProducts[key].SoLuong,
                    LoaiSanPham: listProducts[key].LoaiSanPham,
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

        setProducts([...allProducts]);
    };

    const handleGetCart = async () => {
        const userData = await getUserData();
        // get cart from database and set to redux
        const items = await getCart();
        if (items) {
            for (const key in items[userData.MaNguoiDung]) {
                dispatch(addToCartFromDatabase(items[userData.MaNguoiDung][key]))
            }
        }
    }

    useEffect(() => {
        handleGetProducts();
        handleGetCategories();
        // handleGetCart();
    }, []);

    return (
        <View className="flex-1">
            <ScrollView showsVerticalScrollIndicator={false}>
                <View
                    style={{ height: hp(35) }}
                    className="bg-yellow-950 space-y-5"
                >
                    {/* Header */}
                    <View
                        style={{ width: wp(90) }}
                        className="flex-row justify-between mx-auto mt-20 items-center"
                    >
                        <View>
                            <Text className="text-white">Giao đến</Text>
                            <TouchableOpacity
                                onPress={() => navigation.navigate("MapView")}
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
                            <Image
                                source={require("../assets/images/avtDemo.png")}
                                style={{ width: hp(8), height: hp(8) }}
                            />
                        </View>
                    </View>

                    {/* Search bar */}
                    <View className="mx-5 py-2 px-3 bg-white rounded-lg">
                        <View className="flex-row justify-between">
                            <TextInput
                                placeholder="tìm món..."
                                className="text-lg"
                            />

                            <TouchableOpacity
                                onPress={handleGetProducts}
                                className="p-1 bg-yellow-950 rounded-lg"
                            >
                                <Icons.MagnifyingGlassIcon
                                    size={24}
                                    color="#ffffff"
                                />
                            </TouchableOpacity>
                        </View>
                    </View>
                </View>

                <View className="-mt-20">
                    {/* carousel */}
                    <Carousel
                        loop
                        width={wp(100)}
                        height={hp(22)}
                        autoPlay={true}
                        data={[
                            {
                                id: 1,
                                image: require("../assets/images/Frame 17.png"),
                            },
                            {
                                id: 2,
                                image: require("../assets/images/Frame 17.png"),
                            },
                            {
                                id: 3,
                                image: require("../assets/images/Frame 17.png"),
                            },
                            {
                                id: 4,
                                image: require("../assets/images/Frame 17.png"),
                            },
                        ]}
                        scrollAnimationDuration={1000}
                        renderItem={({ item }) => (
                            <View className="justify-center items-center">
                                <Image
                                    source={item.image}
                                    resizeMode="contain"
                                    style={{ width: wp(90), height: hp(20) }}
                                />
                            </View>
                        )}
                    />
                </View>

                {/* categories */}
                <View className="mx-2">
                    {categories && (
                        <Categories
                            categories={categories}
                            setIsActive={setIsActive}
                        />
                    )}
                </View>

                {/* card item */}
                <View className="mx-5 mt-5 flex-row flex-wrap justify-between">
                    {products && isActive === "Tất cả"
                        ? products
                              .filter((item) => item.SoLuong > 0)
                              .map((product) => {
                                  return (
                                      <Item
                                          product={product}
                                          key={product.MaSanPham}
                                      />
                                  );
                              })
                        : products && products
                              .filter(
                                  (item) =>
                                      item.SoLuong > 0 &&
                                      item.LoaiSanPham == isActive
                              )
                              .map((product, index) => {
                                  return (
                                      <Item
                                          product={product}
                                          key={product.MaSanPham}
                                      />
                                  );
                              })}
                </View>

                {/* out of stock */}
                {products &&
                    products.filter((item) => item.SoLuong == 0).length > 0 && (
                        <View>
                            {/* <Text className="text-center text-lg font-bold mt-5">Hết hàng</Text> */}
                            <View className="mt-5 flex-row justify-between items-center mx-5">
                                <Text
                                    style={{
                                        height: 1,
                                        borderColor: "rgba(59, 29, 12, 0.4)",
                                        borderWidth: 1,
                                        width: wp(30),
                                    }}
                                ></Text>

                                <Text
                                    style={{ color: "#8B6122" }}
                                    className="text-lg font-semibold"
                                >
                                    Hết hàng
                                </Text>

                                <Text
                                    style={{
                                        height: 1,
                                        borderColor: "rgba(59, 29, 12, 0.4)",
                                        borderWidth: 1,
                                        width: wp(30),
                                    }}
                                ></Text>
                            </View>

                            <View className="mx-5 mt-5 flex-row flex-wrap justify-between">
                                {products
                                    .filter((item) => item.SoLuong == 0)
                                    .map((product, index) => {
                                        return (
                                            <Item
                                                product={product}
                                                key={product.MaSanPham}
                                            />
                                        );
                                    })}
                            </View>
                        </View>
                    )}
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
        </View>
    );
};

export default HomeScreen;
