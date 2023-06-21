import { View, Text, FlatList, RefreshControl } from "react-native";
import React, { useEffect, useState, useCallback } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { CardProduct } from "../../product/components/CardProduct";
import { useAppDispatch, useAppSelector } from "../../../store/store";
import { Product } from "./../../../models/Product";
import { fetchProductsAsync } from "../../../store/context/productSlice";

const wait = (timeout: any) => {
  return new Promise((resolve) => setTimeout(resolve, timeout));
};

export const HomeScreen = ({ navigation }: any) => {
  const { products, loading } = useAppSelector((state) => state.product);
  useAppSelector((state) => state.product)
  const [refreshing, setRefreshing] = useState(false);

  const dispatch = useAppDispatch();

  const onRefresh = useCallback(() => {
    setRefreshing(true);
    wait(1000).then(() => setRefreshing(false));
  }, []);

  //console.log("loading : ", loading, "\nproducts", products);

  useEffect(() => {
    dispatch(fetchProductsAsync());
  }, []);

  return (
    <>
      <SafeAreaView style={{ backgroundColor: "#fff", flex: 1 }}>
        <FlatList
          ListHeaderComponent={() => (
            <>
              <Text
                style={{
                  fontFamily: "Comfortaa_500Medium",
                  fontSize: 40,
                  margin: 40,
                  marginLeft: 10,
                }}
              >
                HachiShop
              </Text>
              {/* <Text
                style={{
                  fontFamily: "Comfortaa_500Medium",
                  fontWeight: "bold",
                  marginLeft: 10,
                  marginBottom: 20,
                }}
              >
                CONVERSE
              </Text> */}
            </>
          )}
          data={products}
          renderItem={(item: any) => (
            <CardProduct data={item} navigation={navigation} />
          )}
          keyExtractor={(_, i): any => i}
          numColumns={2}
          refreshControl={
            <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
          }
        />
      </SafeAreaView>
    </>
  );
};
