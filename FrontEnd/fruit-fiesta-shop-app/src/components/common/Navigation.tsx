import { Box } from '@chakra-ui/react';

const Navigation = () => {
  return (
    <Box
      as="header"
      bg="blue.500"
      color="white"
      py="4"
      px="8"
      position="fixed"
      top="0"
      left="0"
      right="0"
      zIndex="999"
    >
      Header Content
    </Box>
  );
};

export default Navigation;