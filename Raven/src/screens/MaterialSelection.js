import React, {useEffect, useState} from 'react';
import {View, Text, StyleSheet, FlatList} from 'react-native';
import ajax from '../ajax';

export default MaterialSelection = (props, {navigation, route}) => {
  const [material, setMaterial] = useState([]);
  const [option, setOption] = useState();
  const distillationOption = props.route.params.data;

  useEffect(() => {
    async function getMaterial() {
      const materialList = await ajax.fetchMaterial();
      return setMaterial(await materialList);
    }
    setOption(distillationOption);
    getMaterial();
  }, []);

  handlePress = selection => {
    props.navigation.push('Product Information', {
      choice: distillationOption,
      data: selection,
    });
  };

  return (
    <View style={styles.container}>
      <View style={styles.title}>
        <Text style={styles.text}>Distillation</Text>
        <Text style={styles.text}>{distillationOption}</Text>
      </View>
      <FlatList
        numColumns={2}
        columnWrapperStyle={{justifyContent: 'space-evenly'}}
        ItemSeparatorComponent={<View style={{margin: '5%'}} />}
        style={styles.buttons}
        data={material}
        renderItem={({item, index}) => (
          <LargeButton
            onPress={() => handlePress(item)}
            key={item.materialNumber}
            name={item.materialName}
          />
        )}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    margin: '5%',
  },
  title: {
    alignItems: 'center',
    justifyContent: 'center',
  },
  text: {
    fontSize: 68,
  },
  buttons: {
    marginTop: '10%',
    flex: 1,
    flexDirection: 'column',
  },
});
