import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Text, View } from 'react-native';
import Navigation from './navigation/Navigation';
import { PaperProvider } from 'react-native-paper';
import Toast from 'react-native-toast-message';

export default function App() {
  return (
    <PaperProvider>
      <Navigation />
      <Toast />
    </PaperProvider>
  );
}
