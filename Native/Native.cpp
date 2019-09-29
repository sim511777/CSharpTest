// Native.cpp : DLL 응용 프로그램을 위해 내보낸 함수를 정의합니다.
//

#include "stdafx.h"
#include "Native.h"

// 내보낸 함수의 예제입니다.
NATIVE_API int Add(int a, int b)
{
    return a + b;
}

NATIVE_API BYTE* NewBuffer(__int64 cb) {
    return new BYTE[cb];
}

NATIVE_API void DeleteBuffer(BYTE* buffer) {
    delete [] buffer;
}

NATIVE_API BYTE* MallocBuffer(__int64 cb) {
    return (BYTE*)malloc(cb);
}

NATIVE_API void FreeBuffer(BYTE* buffer) {
    free(buffer);
}
