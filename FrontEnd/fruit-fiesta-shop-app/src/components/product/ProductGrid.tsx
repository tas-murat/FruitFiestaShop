import React from 'react'
import ProductItem from './ProductItem.tsx'
import { Grid } from '@chakra-ui/react'
import { useQuery } from '@tanstack/react-query';
import {fetchProductList} from '../../services/api.js'
const ProductGrid = () => {
  
 

  const { isPending, error, data } = useQuery({
    queryKey: ['products'],
    queryFn: fetchProductList,
  })

  if (isPending) return 'Loading...'

  if (error) return 'An error has occurred: ' + error.message

  console.log("data",data);
  return (
    <Grid templateColumns={{ base: 'repeat(1, 1fr)', sm: 'repeat(1, 1fr)', md: 'repeat(2, 1fr)', lg: 'repeat(3, 1fr)' , xl: 'repeat(4, 1fr)' }} gap="3rem" padding="1rem">
      {data.result.map((product, index) => (
      <ProductItem key={index} product={product}   />
    ))}
  </Grid>
  )
}



export default ProductGrid;