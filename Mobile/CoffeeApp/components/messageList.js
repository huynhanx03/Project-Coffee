import { View, Text, ScrollView } from "react-native";
import React, { useEffect } from "react";
import MessageItem from "./messageItem";

const MessageList = ({ scrollRef, messages, currentUser }) => {
    return (
        <ScrollView ref={scrollRef} showsVerticalScrollIndicator={false}>
            {
                messages.map((message, index) => {
                    return (
                        currentUser && <MessageItem key={index} message={message} currentUser={currentUser}/>
                    )
                })
            }
        </ScrollView>
    );
};

export default MessageList;
