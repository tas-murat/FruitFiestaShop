import React from 'react'
import { Box, Image, Text, Button, Card, CardBody, Stack, Heading, Divider, CardFooter, ButtonGroup } from '@chakra-ui/react';
const ProductItem = ({ product, isItemOnBasket, addToBasket }) => {
  const { name, price,description, imageUrl } = product;
  return (
    <Card maxW='sm'>
  <CardBody>
    <Image
      src={imageUrl}
          alt={name}
          height='15rem'
          borderRadius='lg'
          loading='lazy'
    />
    <Stack mt='6' spacing='3'>
          <Heading size='md'>{ name}</Heading>
      <Text>
       {description}
      </Text>
      <Text color='blue.600' fontSize='2xl'>
        {price}
      </Text>
    </Stack>
  </CardBody>
  <Divider />
  <CardFooter>
    <ButtonGroup spacing='1'>
      
      <Button variant='ghost' colorScheme='blue'>
        Add to cart
      </Button>
    </ButtonGroup>
  </CardFooter>
</Card>
  )
}
export default ProductItem;