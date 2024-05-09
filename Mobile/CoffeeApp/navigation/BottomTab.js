import { View, Text } from "react-native";
import React from "react";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import HomeScreen from "../screens/HomeScreen";
import { Entypo } from "@expo/vector-icons";
import { Ionicons } from "@expo/vector-icons";
import { AntDesign } from "@expo/vector-icons";
import ProfileScreen from "../screens/ProfileScreen";
import * as Icons from "react-native-heroicons/mini";
import { colors } from "../theme";
import { MaterialIcons } from '@expo/vector-icons';
import ChatScreen from "../screens/ChatScreen";
import MenuScreen from "../screens/MenuScreen";

const Tab = createBottomTabNavigator();

const BottomTab = () => {
    return (
        <Tab.Navigator>
            <Tab.Screen
                name="Home"
                component={HomeScreen}
                options={{
                    headerShown: false,
                    tabBarLabel: "Trang chủ",
                    tabBarLabelStyle: { color: colors.primary },
                    tabBarIcon: ({ focused }) =>
                        focused ? (
                            <Icons.HomeIcon size={30} color={colors.primary} />
                        ) : (
                            <Icons.HomeIcon size={30} color={'gray'} />
                        ),
                }}
            />

            <Tab.Screen
                name="Menu"
                component={MenuScreen}
                options={{
                    headerShown: false,
                    tabBarLabel: "Đồ ăn và nước uống",
                    tabBarLabelStyle: { color: colors.primary },
                    tabBarIcon: ({ focused }) =>
                        focused ? (
                            <MaterialIcons name="fastfood" size={30} color={colors.primary} />
                        ) : (
                            <MaterialIcons name="fastfood" size={30} color={"gray"} />
                        ),
                }}
            />

            <Tab.Screen
                name="Chat"
                component={ChatScreen}
                options={{
                    headerShown: false,
                    tabBarLabel: "Chat",
                    tabBarLabelStyle: { color: colors.primary },
                    tabBarIcon: ({ focused }) =>
                        focused ? (
                            <Icons.ChatBubbleOvalLeftEllipsisIcon size={30} color={colors.primary} />
                        ) : (
                            <Icons.ChatBubbleOvalLeftEllipsisIcon size={30} color="gray" />
                        ),
                }}
            />

            <Tab.Screen
                name="Profile"
                component={ProfileScreen}
                options={{
                    headerShown: false,
                    tabBarLabel: "Thông tin",
                    tabBarLabelStyle: { color: colors.primary },
                    tabBarIcon: ({ focused }) =>
                        focused ? (
                            <Icons.UserCircleIcon size={30} color={colors.primary} />
                        ) : (
                            <Icons.UserCircleIcon size={30} color={'gray'} />
                        ),
                }}
            />
        </Tab.Navigator>
    );
};

export default BottomTab;
