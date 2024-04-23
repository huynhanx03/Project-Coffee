import { View, Text, Platform, ScrollView, KeyboardAvoidingView } from "react-native";
import React from "react";

const ios = Platform.OS === "ios";
const CustomKeyboard = ({ children }) => {
    return (
        <KeyboardAvoidingView behavior={ios ? "padding" : "height"} style={{ flex: 1 }} keyboardVerticalOffset={-30}>
            <ScrollView
                style={{ flex: 1 }}
                bounces={false}
                showsVerticalScrollIndicator={false}
                contentContainerStyle={{ flex: 1 }}>
                {children}
            </ScrollView>
        </KeyboardAvoidingView>
    );
};

export default CustomKeyboard;
