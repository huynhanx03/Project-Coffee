import { useNavigation } from "@react-navigation/native";
import React, { useEffect, useState } from "react";
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
import {
    heightPercentageToDP as hp,
    widthPercentageToDP as wp,
} from "react-native-responsive-screen";
import Categories from "../components/categories";
import Item from "../components/item";
import { colors } from "../theme";
import {
    getCategories,
    getProducts,
    getProductsBestSeller,
} from "../controller/ProductController";
import { useSelector } from "react-redux";
import { SafeAreaView } from "react-native-safe-area-context";
import LottieView from "lottie-react-native";
import Animated, { FadeInUp } from "react-native-reanimated";

const MenuScreen = () => {
    const navigation = useNavigation();
    const [isActive, setIsActive] = useState("Tất cả");
    const [products, setProducts] = useState(null);
    const [categories, setCategories] = useState(null);
    const [proBestSeller, setProBestSeller] = useState([]);
    const [search, setSearch] = useState("");
    const [searchResult, setSearchResult] = useState([]);
    const [searchPage, setSearchPage] = useState(false);


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
                    Mota: listProducts[key].Mota,
                    PhanTramGiam: listProducts[key].PhanTramGiam,
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

    const handleSearch = (text) => {
        setSearch(text);
        
        const result = products.filter((product) => product.TenSanPham.toLowerCase().includes(text.toLowerCase()));
        setSearchResult(result);
    };

    const handleGetBestSeller = async () => {
        const bestSeller = await getProductsBestSeller();
        setProBestSeller(bestSeller);
    };

    const handleOnBlur = () => {
        if (search.length === 0) {
            setSearchPage(false);
            setSearchResult([]);
        }
    }

    useEffect(() => {
        handleGetProducts();
        handleGetCategories();
        handleGetBestSeller();
    }, []);

    return (
        <View className="flex-1">
            <ScrollView showsVerticalScrollIndicator={false}>
                <SafeAreaView
                    style={{ height: hp(22) }}
                    className="bg-yellow-950 space-y-5"
                >
                    <Text className="text-center text-2xl font-bold text-white">
                        EPSRO
                    </Text>
                    {/* Search bar */}
                    <View className="mx-5 bg-white rounded-3xl">
                        <View className="flex-row items-center space-x-3 px-3">
                            <View className="border-r-[1px] space-x-1 border-gray-400">
                                <Icons.MagnifyingGlassIcon
                                    size={24}
                                    color={"gray"}
                                    className="mr-2"
                                />
                            </View>
                            <TextInput
                                keyboardType="web-search"
                                placeholder="tìm món..."
                                onChangeText={(text) => handleSearch(text)}
                                className="text-lg py-4"
                                onFocus={() => setSearchPage(true)}
                                onBlur={handleOnBlur}
                                style={{ lineHeight: wp(5), width: "100%" }}
                            />
                        </View>
                    </View>
                </SafeAreaView>

                {searchPage ? (
                    <View
                        className="rounded-3xl"
                        style={{
                            marginTop: wp(-5),
                            backgroundColor: "#f2f2f2",
                        }}
                    >
                        {
                            searchResult && searchResult.length > 0 ? (
                                <View className="mx-5 mt-5 flex-row flex-wrap justify-between">
                                    {
                                        searchResult.map((product) => {
                                            return (
                                                <Item
                                                    product={product}
                                                    key={product.MaSanPham}
                                                    isSale={
                                                        product.PhanTramGiam > 0
                                                    }
                                                    isBestSeller={proBestSeller.includes(
                                                        product.MaSanPham
                                                    )}
                                                />
                                            );
                                        })
                                    }
                                </View>
                            ) : (
                                <LottieView
                                    source={require("../assets/lottie/Search.json")}
                                    autoPlay
                                    loop
                                    speed={0.7}
                                    style={{
                                        height: hp(50),
                                        width: wp(80),
                                        alignSelf: "center",
                                        marginTop: 50,
                                        justifyContent: "center",
                                    }}
                                />
                            )
                        }
                    </View>
                ) : (
                    <View
                        className="rounded-3xl"
                        style={{
                            marginTop: wp(-5),
                            backgroundColor: "#f2f2f2",
                        }}
                    >
                        <View className="mx-2 mt-5">
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
                                            <Animated.View key={product.MaSanPham} entering={FadeInUp.duration(1500)}>

                                                <Item
                                                    product={product}
                                                    isSale={
                                                        product.PhanTramGiam > 0
                                                    }
                                                    isBestSeller={proBestSeller.includes(
                                                        product.MaSanPham
                                                    )}
                                                />
                                            </Animated.View>
                                          );
                                      })
                                : products &&
                                  products
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
                                                  isSale={
                                                      product.PhanTramGiam > 0
                                                  }
                                                  isBestSeller={proBestSeller.includes(
                                                      product.MaSanPham
                                                  )}
                                              />
                                          );
                                      })}
                        </View>
                        {/* out of stock */}
                        {products &&
                            products.filter((item) => item.SoLuong == 0)
                                .length > 0 && (
                                <View>
                                    {/* <Text className="text-center text-lg font-bold mt-5">Hết hàng</Text> */}
                                    <View className="mt-5 flex-row justify-between items-center mx-5">
                                        <Text
                                            style={{
                                                height: 1,
                                                borderColor:
                                                    "rgba(59, 29, 12, 0.4)",
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
                                                borderColor:
                                                    "rgba(59, 29, 12, 0.4)",
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
                    </View>
                )}

                {/* categories */}
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

export default MenuScreen;
